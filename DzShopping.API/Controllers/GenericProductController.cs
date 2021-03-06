﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DzShopping.API.Dtos;
using DzShopping.API.Errors;
using DzShopping.API.Helpers;
using DzShopping.Core.Models;
using DzShopping.Core.Specifications.SpecificationClasses;
using DzShopping.Infrastructure.Repositories.GenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers
{
    public class GenericProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;

        public GenericProductController(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> brandRepository, IGenericRepository<ProductType> typeRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery] ProductSpecificationParams productParams)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpecification = new ProductWithFiltersForCountSpecification(productParams);
            var totalItemsCount = await _productRepository.CountAsync(countSpecification);

            var products = await _productRepository.GetListWithSpecification(specification);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize,
                totalItemsCount, data));
        }

        [Cached(600)]
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int productId)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productId);

            var product = await _productRepository.GetWithSpecification(specification);

            if (product == null) return NotFound(new ApiResponse(404, $"Product with id: {productId} does not exist"));

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _brandRepository.GetListAllAsync();

            if (productBrands != null && productBrands.Count > 0) return Ok(productBrands);

            return NoContent();
        }

        [Cached(600)]
        [HttpGet("brands/{productBrandId}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int productBrandId)
        {
            var productBrand = await _brandRepository.GetByIdAsync(productBrandId);

            if (productBrand == null)
                return NotFound(new ApiResponse(404, $"Product brand with id: {productBrandId} does not exist"));

            return Ok(productBrand);
        }

        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _typeRepository.GetListAllAsync();

            if (productTypes != null && productTypes.Count > 0) return Ok(productTypes);

            return NoContent();
        }

        [Cached(600)]
        [HttpGet("types/{productTypeId}")]
        public async Task<ActionResult<ProductType>> GetProductType(int productTypeId)
        {
            var productType = await _typeRepository.GetByIdAsync(productTypeId);

            if (productType == null)
                return NotFound(new ApiResponse(404, $"Product type with id: {productTypeId} does not exist"));

            return Ok(productType);
        }
    }
}
