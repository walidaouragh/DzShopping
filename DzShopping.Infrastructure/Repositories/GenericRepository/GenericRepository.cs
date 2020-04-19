using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DzShopping.Core.Specifications;
using DzShopping.Infrastructure.DbContext;
using DzShopping.Infrastructure.SpecificationEvaluator;
using Microsoft.EntityFrameworkCore;

namespace DzShopping.Infrastructure.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DzDbContext _dzDbContext;

        public GenericRepository(DzDbContext dzDbContext)
        {
            _dzDbContext = dzDbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dzDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetListAllAsync()
        {
            return await _dzDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetWithSpecification(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetListWithSpecification(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_dzDbContext.Set<T>().AsQueryable(),
                specification);
        }
    }
}