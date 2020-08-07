﻿using Microsoft.EntityFrameworkCore;
using CasSys.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasSys.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .ToTable("User");

            builder
                .Property(x => x.FirstName).HasMaxLength(60);

            builder
                .Property(x => x.LastName).HasMaxLength(60);

            builder
                .Property(x => x.Gender).HasMaxLength(10);

            builder
                .Property(x => x.LockoutEnd).HasColumnType("datetime");

            builder
                .Property(m => m.NormalizedEmail).HasMaxLength(127);

            builder
                .Property(m => m.NormalizedUserName).HasMaxLength(127);
        }
    }
}
