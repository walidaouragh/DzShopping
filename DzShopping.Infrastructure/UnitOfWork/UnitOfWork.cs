using DzShopping.Infrastructure.DbContext;
using DzShopping.Infrastructure.Repositories.GenericRepository;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.UnitOfWork
{
    // unit of work purpose is to use only one db instance.
    // to avoid partial db save (for instance save delivery method but not address because there was a problem with address only)
    // with unit of work we make sure everything is correct to save everything just one time.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DzDbContext _dzDbContext;
        private Hashtable _repositories;

        public UnitOfWork(DzDbContext dzDbContext)
        {
            _dzDbContext = dzDbContext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dzDbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public async Task<int> Complete()
        {
            return await _dzDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dzDbContext.Dispose();
        }   
    }
}
