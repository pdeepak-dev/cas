using System;
using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.RequestModels
{
    public class JobUpdateRequestModel : JobBaseRequestModel
    {
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Type { get; set; }

        public DateTime LastDate { get; set; }

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string Website { get; set; }
    }
}