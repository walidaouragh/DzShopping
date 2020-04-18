using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DzShopping.Core.Specifications
{
    // This logic for adding "include" to add tables when querying (Generic repository) and passing ids that we want to get
    public class Specification<T> : ISpecification<T>
    {
        public Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        // criteria is for the ids
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}