using CasSys.Application.Jwt;
using CasSys.Infrastructure.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CasSys.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // jwt
            services.TryAddSingleton<IJwtTokenHandler, JwtTokenHandler>();
            services.TryAddSingleton<IJwtFactory, JwtFactory>();
            services.TryAddSingleton<ITokenFactory, TokenFactory>();
            services.TryAddSingleton<IJwtTokenValidator, JwtTokenValidator>();
            services.TryAddScoped<IJwtHttpContext, JwtHttpContext>();

            return services;
        }
    }
}
