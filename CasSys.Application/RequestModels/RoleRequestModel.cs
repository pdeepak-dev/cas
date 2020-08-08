using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.RequestModels
{
    public class RoleRequestModel
    {
        [Required(ErrorMessage = "Please provider role name", AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}