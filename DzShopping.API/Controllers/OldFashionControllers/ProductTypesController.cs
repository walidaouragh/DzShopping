using System.Collections.Generic;
using System.Threading.Tasks;
using DzShopping.Core.Models;
using DzShopping.Infrastructure.Repositories.ProductTypeRepository;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers
{
    [Route("api/productTypes")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypesController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepository.GetProductTypes();

            if (productTypes != null && productTypes.Count > 0) return Ok(productTypes);

            return NoContent();
        }

        [HttpGet("{productTypeId}")]
        public async Task<ActionResult<ProductType>> GetProductType(int productTypeId)
        {
            var productType = await _productTypeRepository.GetProductType(productTypeId);

            if (productType == null) return NotFound($"Product type with id: {productTypeId} does not exist");

            return Ok(productType);
        }
    }
}
