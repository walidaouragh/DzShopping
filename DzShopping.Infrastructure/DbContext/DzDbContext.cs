using System.Reflection;
using DzShopping.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DzShopping.Infrastructure.DbContext
{
    public class DzDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DzDbContext(DbContextOptions<DzDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
