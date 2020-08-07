using CasSys.Domain.EntityFrameworkCore.Repositories.Core;
using CasSys.Persistence.EntityFrameworkCore.Repositories.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CasSys.Persistence.Integrations
{
    public static class RepositoryServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the repository as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}