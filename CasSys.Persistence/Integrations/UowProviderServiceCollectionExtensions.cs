using CasSys.Domain.EntityFrameworkCore.UnitOfWorks;
using CasSys.Persistence.EntityFrameworkCore.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CasSys.Persistence.Integrations
{
    public static class UowProviderServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the unit of work provider as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddUowProvider(this IServiceCollection services)
        {
            services.TryAddScoped<IUowProvider, UowProvider>();

            return services;
        }
    }
}