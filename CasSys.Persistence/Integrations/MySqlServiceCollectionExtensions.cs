using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CasSys.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using CasSys.Persistence.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace CasSys.Persistence.Integrations
{
    public static class MySqlServiceCollectionExtensions
    {
        public static IServiceCollection AddMySqlDbContext
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("CasSystemConnection"),
                    b => b.CharSetBehavior(CharSetBehavior.NeverAppend) // <-- Don't append utf8mb4
                          .MigrationsAssembly(assemblyName: "CasSys.Persistence"));
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 6,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false,
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            return services;
        }
    }
}