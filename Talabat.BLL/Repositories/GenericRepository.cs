﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specification;
using Talabat.DAL.Data;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
        }

        public void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public  async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifictions(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecifictions(spec).FirstOrDefaultAsync();
        }

        public void Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
        }

        private IQueryable<T> ApplySpecifictions(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(),specification);
        }
    }
}
