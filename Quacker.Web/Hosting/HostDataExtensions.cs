using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quacker.Dal.SeedInterfaces;

namespace Quacker.Web.Hosting
{
    public static class HostDataExtensions
    {
        public static async Task<IHost> MigrateDatabaseAsync<TContext>(this IHost host)
                where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<TContext>();

                // Update DB
                context.Database.Migrate();

                // Creating Role and User
                var roleSeeder = serviceProvider.GetRequiredService<IRoleSeedService>();
                await roleSeeder.SeedRoleAsync();

                var userSeeder = serviceProvider.GetRequiredService<IUserSeedService>();
                await userSeeder.SeedUserAsync();

            }
            return host;
        }

    }
}
