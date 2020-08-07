using System;
using CasSys.Domain.Entities.Core;
using CasSys.Domain.Entities.Identity;

namespace CasSys.Domain.Entities
{
    public class Applicant : Entity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int JobId { get; set; }

        public DateTime CreatedAt { get; set; }

        // -----------------------------------------------
        // Relationships

        public AppUser User { get; set; }
        public Job Job { get; set; }
    }
}