using Intern.NTQ.Infrastructure.Common.BaseRepository;
using Intern.NTQ.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Repositories.ProductRepository
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product> GetByProductID(int? id);
        Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex);
        Task<List<Product>> GetAllProduct(int? pageSize, int? pageIndex, Expression<Func<Product, bool>> expression);
    }
}
