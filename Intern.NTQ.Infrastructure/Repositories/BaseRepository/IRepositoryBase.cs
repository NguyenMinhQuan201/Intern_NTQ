using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Common.BaseRepository
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAll(int? pageSize, int? pageIndex, Expression<Func<T, bool>> expression);
        Task<List<T>> GetAll(int? pageSize, int? pageIndex);
        Task<List<T>> GetAllInclue(int? pageSize, int? pageIndex, Expression<Func<T, bool>> expressionWhere, Expression<Func<T, ICollection<T>>> expressionInclue);
        Task<List<T>> GetAllInclue(int? pageSize, int? pageIndex, Expression<Func<T, ICollection<T>>> expressionInclue);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetById(Expression<Func<T, bool>> expression);
        Task<T> GetById(int id);
        Task<List<T>> GetListByCondition(Expression<Func<T, bool>> expression);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task CreateAsync(T entity);
    }

}
