using System;
using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.RequestModels
{
    public class JobRequestModel
    {
        [Required(ErrorMessage = "Please provide job title", AllowEmptyStrings = false)]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please provide job description", AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please provide Location", AllowEmptyStrings = false)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please provide job type")]
        public string Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please provide last date")]
        public DateTime LastDate { get; set; }

        [Required(ErrorMessage = "Please provide company name", AllowEmptyStrings = false)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please provide company description", AllowEmptyStrings = false)]
        public string CompanyDescription { get; set; }

        public string Website { get; set; }
    }
}