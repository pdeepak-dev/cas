using System.Security.Claims;

namespace CasSys.Application.Jwt
{
    public interface IJwtTokenValidator
    {
        ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
    }
}