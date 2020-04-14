using DzShopping.Core.Models;
using DzShopping.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.Repositories.ProductTypeRepository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly DzDbContext _dzDbContext;

        public ProductTypeRepository(DzDbContext dzDbContext)
        {
            _dzDbContext = dzDbContext;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypes()
        {
            return await _dzDbContext.ProductTypes.ToListAsync();
        }

        public async Task<ProductType> GetProductType(int productTypeId)
        {
            return await _dzDbContext.ProductTypes.Where(id => id.ProductTypeId == productTypeId)
                .FirstOrDefaultAsync();
        }
    }
}
