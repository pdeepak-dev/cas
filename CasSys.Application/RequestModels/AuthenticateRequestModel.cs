using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.RequestModels
{
    public class AuthenticateRequestModel
    {
        [Required(ErrorMessage = "Please provide email address", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
