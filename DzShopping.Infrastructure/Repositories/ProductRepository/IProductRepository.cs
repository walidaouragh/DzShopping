using DzShopping.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProduct(int productId);
        IQueryable<Product> GetProducts();
    }
}
