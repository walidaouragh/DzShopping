using DzShopping.Core.Models;

namespace DzShopping.Core.Specifications.SpecificationClasses
{
    public class ProductsWithTypesAndBrandsSpecification : Specification<Product>
    {
        //without passing id, get everything
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.ProductName.ToLower().Contains(productParams.Search)) &&
            (productParams.BrandId == null || x.ProductBrand.ProductBrandId == productParams.BrandId) &&
            (productParams.TypeId == null || x.ProductType.ProductTypeId == productParams.TypeId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.ProductName);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.ProductName);
                        break;
                }
        }

        //passing id
        public ProductsWithTypesAndBrandsSpecification(int productId) : base(p => p.ProductId == productId)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}