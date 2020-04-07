using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DzShopping.Core.Models;
using DzShopping.Infrastructure.Repositories.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            List<Product> products = await _productRepository.GetProducts().ToListAsync();

            if (products != null && products.Count > 0)
            {
                return Ok(products);
            }

            return NoContent();
        }

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<Product>> GetProduct(int ProductId)
        {
            var product = await _productRepository.GetProduct(ProductId).SingleOrDefaultAsync();

            if (product == null)
            {
                return NotFound($"Product with id: {ProductId} does not exist");
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
