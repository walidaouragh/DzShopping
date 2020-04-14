using DzShopping.Core.Models;
using DzShopping.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DzDbContext _dzDbContext;

        public ProductRepository(DzDbContext dzDbContext)
        {
            _dzDbContext = dzDbContext;
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            return await _dzDbContext.Products
                .Include(b => b.ProductBrand)
                .Include(t => t.ProductType)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await _dzDbContext.Products
                .Include(b => b.ProductBrand)
                .Include(t => t.ProductType)
                .Where(id => id.ProductId == productId)
                .FirstOrDefaultAsync();
        }
    }
}
