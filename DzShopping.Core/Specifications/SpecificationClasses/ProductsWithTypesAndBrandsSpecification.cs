using DzShopping.Core.Models;

namespace DzShopping.Core.Specifications.SpecificationClasses
{
    public class ProductsWithTypesAndBrandsSpecification : Specification<Product>
    {
        //without passing id, get everything
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        //passing id
        public ProductsWithTypesAndBrandsSpecification(int productId) : base(p => p.ProductId == productId)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}