

using Intern.NTQ.Infrastructure.Common.BaseRepository;
using Intern.NTQ.Infrastructure.Entities;

namespace Intern.NTQ.Infrastructure.Reponsitories.ProductImageReponsitories
{
    public interface IProductImageRepository : IRepositoryBase<ProductImg>
    {
        public Task<IEnumerable<ProductImg>> GetAllProduct(int? id);
    }
}
