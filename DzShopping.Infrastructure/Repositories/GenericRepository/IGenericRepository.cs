﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DzShopping.Core.Specifications;

namespace DzShopping.Infrastructure.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListAllAsync();

        Task<T> GetWithSpecification(ISpecification<T> specification);
        Task<IReadOnlyList<T>> GetListWithSpecification(ISpecification<T> specification);
    }
}