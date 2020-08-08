using System;
using CasSys.WebApi.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CasSys.WebApi.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        private string[] ApiKeyHeaderNames = { "X-ApiKey", "ApiKey", "x-api-key" };
        private readonly IOptions<AppSettings> _appSettings;

        public ApiKeyAuthAttribute(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var ctx = context.HttpContext;

            StringValues apiKey;

            foreach (var apiKeyHeaderName in ApiKeyHeaderNames)
            {
                if (ctx.Request.Headers.TryGetValue(apiKeyHeaderName, out apiKey))
                    break;
            }

            if (apiKey.Count > 0)
            {
                var configuredApiKeyValue = _appSettings?.Value?.ApiKey ?? string.Empty;

                if (!apiKey.Equals(configuredApiKeyValue))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}