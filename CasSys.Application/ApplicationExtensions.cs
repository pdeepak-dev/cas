using AutoMapper;
using System.Reflection;
using CasSys.Application.BizServices;
using Microsoft.Extensions.DependencyInjection;
using CasSys.Application.BizServices.Interfaces;

namespace CasSys.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly());

            services
                .AddScoped<IUserManagementService, UserManagementService>();

            return services;
        }
    }
}