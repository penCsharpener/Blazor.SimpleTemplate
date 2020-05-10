using Blazor.SimpleTemplate.Extensions;
using Blazor.SimpleTemplate.Models.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Blazor.SimpleTemplate {
    public class Program {

        public static IConfiguration LaunchSettings { get; private set; }

        public async static Task Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
#if !DEBUG
                .WriteTo.RollingFile("bin/serilog.log", fileSizeLimitBytes: 2097152)
#endif
                .CreateLogger();

            try {
                Log.Information("Application started.");

                LaunchSettings = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", false, true)
                    .AddUserSecrets<Program>(true, true)
                    .Build();

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope()) {
                    var sp = scope.ServiceProvider;
                    try {
                        await sp.MigrateDatabaseAsync();
                        await sp.SeedDatabaseAsync();
                    } catch (Exception ex) {
                        var logger = sp.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while migrating the database.");
                    }
                }

                host.Run();

            } catch (Exception ex) {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            } finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>()
                    // the two lines below prevent ef migrations to be added via PM console
#if !DEBUG
                        .UseKestrel()
                        .UseUrls(LaunchSettings.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>().LaunchUrls)
#endif
                    ;
                });
    }
}
