using System.Reflection;
using DzShopping.Core.Models;
using DzShopping.Core.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace DzShopping.Infrastructure.DbContext
{
    public class DzDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DzDbContext(DbContextOptions<DzDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
