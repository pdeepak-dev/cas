using Microsoft.EntityFrameworkCore;
using CasSys.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CasSys.Persistence.EntityFrameworkCore
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfigurationsFromAssembly(assembly: this.GetType().Assembly);
        }
    }
}