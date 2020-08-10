using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.RequestModels
{
    public class UserRequestModel
    {
        [MaxLength(60)]
        public string UserName { get; set; }

        [MaxLength(60)]
        [Required(ErrorMessage = "Please provider first name")]
        public string FirstName { get; set; }

        [MaxLength(60)]
        [Required(ErrorMessage = "Please provider last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please provider gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please provider email address")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provider password")]
        public string Password { get; set; }

        public bool IsEmployee = false;
    }
}
