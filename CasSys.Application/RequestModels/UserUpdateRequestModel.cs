using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.RequestModels
{
    public class UserUpdateRequestModel : UserBaseRequestModel
    {
        [MaxLength(60)]
        [Required(ErrorMessage = "Please provider first name")]
        public string FirstName { get; set; }

        [MaxLength(60)]
        [Required(ErrorMessage = "Please provider last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please provider gender")]
        public string Gender { get; set; }
    }
}