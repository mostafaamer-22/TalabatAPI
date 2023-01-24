﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specification;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Interfaces
{
    public interface IGenericRepository<T>  where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        void Add(T Entity);
        void Delete(T Entity);
        void Update(T Entity);
    }
}
