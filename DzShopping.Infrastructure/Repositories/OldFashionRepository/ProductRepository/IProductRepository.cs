using DzShopping.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.Repositories.OldFashionRepository.ProductRepository
{
    public interface IProductRepository
    {
        Task<Product> GetProduct(int productId);
        Task<IReadOnlyList<Product>> GetProducts();
    }
}
