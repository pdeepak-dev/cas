using CasSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasSys.Persistence.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder
                .ToTable("Jobs");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Title).HasMaxLength(200).IsRequired();

            builder
                .Property(x => x.Location).HasMaxLength(100);

            builder
                .Property(x => x.Type).HasMaxLength(60).IsRequired();

            builder
                .Property(x => x.CreatedAt).HasColumnType("datetime");

            builder
                .Property(x => x.LastDate).HasColumnType("datetime");

            builder
                .Property(x => x.CompanyName).HasMaxLength(500).IsRequired();

            builder
                .Property(x => x.Website).IsUnicode(false);
        }
    }
}