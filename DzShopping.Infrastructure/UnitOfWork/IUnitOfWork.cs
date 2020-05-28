using DzShopping.Infrastructure.Repositories.GenericRepository;
using System;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
