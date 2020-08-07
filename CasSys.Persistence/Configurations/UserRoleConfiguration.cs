using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasSys.Persistence.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder
                .ToTable("UserRoles");

            builder
                .Property(m => m.UserId).HasMaxLength(127);

            builder
                .Property(m => m.RoleId).HasMaxLength(127);
        }
    }
}