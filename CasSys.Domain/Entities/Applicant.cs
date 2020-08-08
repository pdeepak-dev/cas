using System;
using CasSys.Domain.Entities.Core;
using CasSys.Domain.Entities.Identity;

namespace CasSys.Domain.Entities
{
    public class Applicant : IAggregateRoot
    {
        public int JobId { get; set; }
        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        // -----------------------------------------------
        // Relationships

        public AppUser User { get; set; }
        public Job Job { get; set; }
    }
}