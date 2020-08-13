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
                .ToTable("Applicants");

            builder
                .HasKey(x => new { x.UserId, x.JobId });

            builder
                .Property(x => x.CreatedAt).HasColumnType("datetime");

            // Relationships

            builder
                .HasOne(p => p.Job)
                    .WithMany(p => p.Applicants).HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.User)
                    .WithMany(p => p.Applicants).HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}