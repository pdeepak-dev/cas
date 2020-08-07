using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasSys.Persistence.Configurations
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder
                .ToTable("UserClaims");

            builder
                .Property(m => m.UserId).HasMaxLength(127);
        }
    }
}