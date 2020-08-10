using System.Collections.Generic;

namespace CasSys.Application.Jwt
{
    public class JwtUserResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsThirdPartyClient { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}