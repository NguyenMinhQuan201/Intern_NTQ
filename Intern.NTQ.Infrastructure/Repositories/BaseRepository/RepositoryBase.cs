using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Intern.NTQ.Infrastructure.Common.BaseRepository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly NTQDbContext _db;
        public RepositoryBase(NTQDbContext db)
        {
            _db = db;
        }
        public async Task<List<T>> GetAll(int? pageSize, int? pageIndex, Expression<Func<T, bool>> expression)
        {
            var query = _db.Set<T>().Where(expression).AsQueryable();
            var pageCount = query.Count();
                query =  query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
            return await query.ToListAsync();
        }
        public async Task<List<T>> GetAll(int? pageSize, int? pageIndex)
        {
            var query = _db.Set<T>().AsQueryable();
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();
        }
        public async Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            var query = await _db.Set<T>().Where(expression).ToListAsync();
            return query;
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();

        }

        public async Task CreateAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> expression)
        {
            var query = await _db.Set<T>().Where(expression).FirstOrDefaultAsync();
            return query;
        }
        public async Task<T> GetById(int id)
        {
            var query = await _db.Set<T>().FindAsync(id);
            return query;
        }

        public async Task<int> CountAsync()
        {
            var query = await _db.Set<T>().CountAsync();
            return query;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            var query = await _db.Set<T>().Where(expression).CountAsync();
            return query;
        }

        public Task<List<T>> GetAllInclue(int? pageSize, int? pageIndex, Expression<Func<T, bool>> expressionWhere, Expression<Func<T, ICollection<T>>> expressionInclue)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllInclue(int? pageSize, int? pageIndex, Expression<Func<T, ICollection<T>>> expressionInclue)
        {
            throw new NotImplementedException();
        }

        Task<T> IRepositoryBase<T>.GetByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
