using System;
using CasSys.Application.Jwt;
using System.Security.Cryptography;

namespace CasSys.Infrastructure.Jwt
{
    public class TokenFactory : ITokenFactory
    {
        public string GenerateToken(int size = 32)
        {
            var data = new byte[size];

            using (var _rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                _rngCryptoServiceProvider.GetBytes(data);

                return Convert.ToBase64String(data);
            }
        }
    }
}