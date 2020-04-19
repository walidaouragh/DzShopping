using System.Linq;
using DzShopping.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DzShopping.Infrastructure.SpecificationEvaluator
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria); // example when : p => p.ProductId == id

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy); // example when : p => p.ProductName

            if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending); // descending by price for instance!

            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include)); // example when : include(p => p.productBrand)

            return query;
        }
    }
}