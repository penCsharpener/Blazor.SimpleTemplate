using Blazor.SimpleTemplate.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Blazor.SimpleTemplate.Extensions {
    public static class ServiceExtensions {
        public static async Task MigrateDatabaseAsync(this IServiceProvider sp) {
            await sp.GetService<AppDbContext>().Database.MigrateAsync();
        }
    }
}
