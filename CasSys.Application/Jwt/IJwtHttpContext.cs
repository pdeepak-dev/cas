namespace CasSys.Application.Jwt
{
    public interface IJwtHttpContext
    {
        JwtUserResource GetJwtUserResource();
    }
}