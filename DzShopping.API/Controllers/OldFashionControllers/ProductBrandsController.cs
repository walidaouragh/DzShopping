using System.Collections.Generic;
using System.Threading.Tasks;
using DzShopping.Core.Models;
using DzShopping.Infrastructure.Repositories.OldFashionRepository.ProductBrandRepository;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers
{
    [Route("api/productBrands")]
    [ApiController]
    public class ProductBrandsController : ControllerBase
    {
        private readonly IProductBrandRepository _productBrandRepository;

        public ProductBrandsController(IProductBrandRepository productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            IReadOnlyList<ProductBrand> productBrands = await _productBrandRepository.GetProductBrands();

            if (productBrands != null && productBrands.Count > 0)
            {
                return Ok(productBrands);
            }

            return NoContent();
        }

        [HttpGet("{productBrandId}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int productBrandId)
        {
            var productBrand = await _productBrandRepository.GetProductBrand(productBrandId);

            if (productBrand == null)
            {
                return NotFound($"Product brand with id: {productBrandId} does not exist");
            }

            return Ok(productBrand);
        }
    }
}