using System.Collections.Generic;
using System.Threading.Tasks;
using DzShopping.Core.Models;
using DzShopping.Core.Specifications.SpecificationClasses;
using DzShopping.Infrastructure.Repositories.GenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers
{
    [Route("api/genericProduct")]
    [ApiController]
    public class GenericProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository;

        public GenericProductController(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> brandRepository, IGenericRepository<ProductType> typeRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var specification = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepository.GetListWithSpecification(specification);

            if (products != null && products.Count > 0) return Ok(products);

            return NoContent();
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productId);

            var product = await _productRepository.GetWithSpecification(specification);

            if (product == null) return NotFound($"Product with id: {productId} does not exist");

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _brandRepository.GetListAllAsync();

            if (productBrands != null && productBrands.Count > 0) return Ok(productBrands);

            return NoContent();
        }

        [HttpGet("brands/{productBrandId}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int productBrandId)
        {
            var productBrand = await _brandRepository.GetByIdAsync(productBrandId);

            if (productBrand == null) return NotFound($"Product brand with id: {productBrandId} does not exist");

            return Ok(productBrand);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _typeRepository.GetListAllAsync();

            if (productTypes != null && productTypes.Count > 0) return Ok(productTypes);

            return NoContent();
        }

        [HttpGet("types/{productTypeId}")]
        public async Task<ActionResult<ProductType>> GetProductType(int productTypeId)
        {
            var productType = await _typeRepository.GetByIdAsync(productTypeId);

            if (productType == null) return NotFound($"Product type with id: {productTypeId} does not exist");

            return Ok(productType);
        }
    }
}
