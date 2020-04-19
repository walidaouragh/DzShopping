using DzShopping.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.Repositories.OldFashionRepository.ProductBrandRepository
{
    public interface IProductBrandRepository
    {
        Task<IReadOnlyList<ProductBrand>> GetProductBrands();
        Task<ProductBrand> GetProductBrand(int productBrandId);
    }
}
