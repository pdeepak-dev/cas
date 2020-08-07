using System;
using CasSys.Persistence.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace CasSys.WebApi.Helpers.DatabaseSetup
{
    public static class DatabaseStartupHelpers
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                using (var context = services.GetAppDbContext())
                {
                    try
                    {
                        context.MigrateDb();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while migrating the database.");
                    }
                }
            }

            return host;
        }
    }
}