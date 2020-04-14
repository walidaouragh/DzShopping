using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DzShopping.Core.Models;
using DzShopping.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DzShopping.Infrastructure.Repositories.ProductBrandRepository
{
    public class ProductBrandRepository : IProductBrandRepository
    {
        private readonly DzDbContext _dzDbContext;

        public ProductBrandRepository(DzDbContext dzDbContext)
        {
            _dzDbContext = dzDbContext;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            return await _dzDbContext.ProductBrands.ToListAsync();
        }

        public async Task<ProductBrand> GetProductBrand(int productBrandId)
        {
            return await _dzDbContext.ProductBrands.Where(id => id.ProductBrandId == productBrandId)
                .FirstOrDefaultAsync();
        }
    }
}