using System.Linq;
using System.Security.Claims;
using CasSys.Application.Jwt;
using Microsoft.AspNetCore.Http;

namespace CasSys.Infrastructure.Jwt
{
    public class JwtHttpContext : IJwtHttpContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public JwtUserResource GetJwtUserResource()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;

            var nameIdentifier = claims.First(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = claims.First(x => x.Type == ClaimTypes.Name)?.Value;
            var email = claims.First(x => x.Type == ClaimTypes.Email)?.Value;
            var roles = claims.Where(x => x.Type == ClaimTypes.Role);

            return new JwtUserResource
            {
                Id = nameIdentifier,
                Name = name,
                Email = email,
                Roles = roles.Count() > 0 ? roles.Select(x => x.Value).ToArray() : Enumerable.Empty<string>()
            };
        }
    }
}
