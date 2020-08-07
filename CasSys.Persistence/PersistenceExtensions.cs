using CasSys.Persistence.Integrations;
using Microsoft.Extensions.Configuration;
using CasSys.Persistence.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CasSys.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMySqlDbContext(configuration);

            services.AddUowProvider();

            services.AddUnitOfWork<AppIdentityDbContext>();

            services.AddRepositories();

            return services;
        }
    }
}