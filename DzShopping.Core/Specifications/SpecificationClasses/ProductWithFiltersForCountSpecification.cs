﻿using DzShopping.Core.Models;

namespace DzShopping.Core.Specifications.SpecificationClasses
{
    public class ProductWithFiltersForCountSpecification : Specification<Product>
    {
        //To get count of items
        public ProductWithFiltersForCountSpecification(ProductSpecificationParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.ProductName.ToLower().Contains(productParams.Search)) &&
            (productParams.BrandId == null || x.ProductBrand.ProductBrandId == productParams.BrandId) &&
            (productParams.TypeId == null || x.ProductType.ProductTypeId == productParams.TypeId))
        {
        }
    }
}
