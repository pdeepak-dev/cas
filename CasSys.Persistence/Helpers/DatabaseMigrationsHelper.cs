using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CasSys.Persistence.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CasSys.Persistence.Helpers
{
    public static class DatabaseMigrationsHelper
    {
        public static void MigrateDb(this AppIdentityDbContext context)
        {
            var migrationsNeeded = context.Database.GetPendingMigrations().Any();

            if (migrationsNeeded)
            {
                context.Database.Migrate();
            }
        }

        public static AppIdentityDbContext GetAppDbContext(this IServiceProvider serviceProviders)
        {
            return serviceProviders.GetRequiredService<AppIdentityDbContext>();
        }
    }
}