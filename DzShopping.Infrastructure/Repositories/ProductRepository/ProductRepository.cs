using DzShopping.Core.Models;
using DzShopping.Infrastructure.DbContext;
using System;
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
        public IQueryable<Product> GetProducts()
        {
            return _dzDbContext.Products;
        }

        public IQueryable<Product> GetProduct(int productId)
        {
            return _dzDbContext.Products.Where(x => x.ProductId == productId);
        }
    }
}
