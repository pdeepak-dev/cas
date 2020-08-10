using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.RequestModels
{
    public class ApplicantRequestModel
    {
        [Required(ErrorMessage = "Please provide job id")]
        public int JobId { get; set; }
    }
}