using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Blazor.SimpleTemplate.Areas.Identity.IdentityHostingStartup))]
namespace Blazor.SimpleTemplate.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup {
        public void Configure(IWebHostBuilder builder) {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}