using System.Collections.Generic;
using System.Threading.Tasks;
using DzShopping.Core.Models;
using DzShopping.Infrastructure.Repositories.ProductRepository;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();

            if (products != null && products.Count > 0) return Ok(products);

            return NoContent();
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var product = await _productRepository.GetProduct(productId);

            if (product == null) return NotFound($"Product with id: {productId} does not exist");

            return Ok(product);
        }
    }
}
