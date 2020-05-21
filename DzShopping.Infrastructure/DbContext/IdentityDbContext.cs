using DzShopping.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DzShopping.Infrastructure.DbContext
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This to override default AspNetUsers table name to be DzShoppingUsers
            modelBuilder.Entity<AppUser>(
                entity => entity.ToTable(name: "DzShoppingUsers"));
        }

    }
}
