using System.Collections.Generic;

namespace CasSys.Application.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsThirdPartyClient { get; set; }
    }
}