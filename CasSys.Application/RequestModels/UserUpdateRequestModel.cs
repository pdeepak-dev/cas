using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.RequestModels
{
    public class UserUpdateRequestModel : UserBaseRequestModel
    {
        [MaxLength(60)]
        public string FirstName { get; set; }

        [MaxLength(60)]
        public string LastName { get; set; }

        public string Gender { get; set; }
    }
}