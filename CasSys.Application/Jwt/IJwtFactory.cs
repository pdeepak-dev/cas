using System.Threading.Tasks;

namespace CasSys.Application.Jwt
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(JwtUserResource jwtUserResource);
    }
}