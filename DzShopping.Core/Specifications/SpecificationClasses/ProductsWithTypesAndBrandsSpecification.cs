using DzShopping.Core.Models;

namespace DzShopping.Core.Specifications.SpecificationClasses
{
    public class ProductsWithTypesAndBrandsSpecification : Specification<Product>
    {
        //without passing id, get everything
        public ProductsWithTypesAndBrandsSpecification(string sort)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.ProductName);

            if (!string.IsNullOrEmpty(sort))
                switch (sort)
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