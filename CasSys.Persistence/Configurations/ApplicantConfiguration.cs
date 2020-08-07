using CasSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasSys.Persistence.Configurations
{
    public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder
                .HasKey(x => new { x.Id });

            builder
                .Property(x => x.CreatedAt).HasColumnType("datetime");
        }
    }
}