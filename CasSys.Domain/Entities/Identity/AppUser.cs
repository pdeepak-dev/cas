using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CasSys.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        // -----------------------------------------------
        // Relationships

        public ICollection<Applicant> Applicants { get; set; }
    }
}