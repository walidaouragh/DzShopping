using DzShopping.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DzShopping.Infrastructure.DbContext
{
    public class DzDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DzDbContext(DbContextOptions<DzDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
