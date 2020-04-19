using System.Collections.Generic;
using System.Threading.Tasks;
using DzShopping.Core.Models;

namespace DzShopping.Infrastructure.Repositories.OldFashionRepository.ProductTypeRepository
{
    public interface IProductTypeRepository
    {
        Task<ProductType> GetProductType(int productTypeId);
        Task<IReadOnlyList<ProductType>> GetProductTypes();
    }
}