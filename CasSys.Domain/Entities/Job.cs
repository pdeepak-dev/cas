using System;
using CasSys.Domain.Entities.Core;
using CasSys.Domain.Entities.Identity;

namespace CasSys.Domain.Entities
{
    public class Job : Entity
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

        public AppUser User { get; set; }
    }
}