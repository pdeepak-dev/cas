using System;
using System.Collections;
using System.Collections.Generic;

namespace CasSys.Application.Dtos
{
    public class JobDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Type { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastDate { get; set; }

        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string Website { get; set; }

        public bool IsFilled { get; set; }

        public int UserId { get; set; }

        // -----------------------------------------------
        // Relationships

        public UserDto User { get; set; }
        public IList<ApplicantDto> Applicants { get; set; }
    }
}
