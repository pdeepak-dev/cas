namespace CasSys.Application.Jwt
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}