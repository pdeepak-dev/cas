using CasSys.Application.Jwt;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace CasSys.WebApi.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static JwtUserResource GetJwtUserResource(this HttpContext ctx)
        {
            var claims = ctx.User.Claims;

            var nameIdentifier = claims.First(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = claims.First(x => x.Type == ClaimTypes.Name)?.Value;
            var email = claims.First(x => x.Type == ClaimTypes.Email)?.Value;

            return new JwtUserResource
            {
                Id = nameIdentifier,
                Name = name,
                Email = email
            };
        }
    }
}
