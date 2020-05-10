using System;
using System.Threading.Tasks;

namespace Blazor.SimpleTemplate.Extensions {
    public static class SeedingExtensions {
        public static Task SeedDatabaseAsync(this IServiceProvider provider) {
            return Task.CompletedTask;
        }

    }
}
