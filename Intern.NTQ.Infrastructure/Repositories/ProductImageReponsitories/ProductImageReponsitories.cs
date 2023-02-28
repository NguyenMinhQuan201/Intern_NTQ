using Infrastructure.EF;
using Intern.NTQ.Infrastructure.Common.BaseRepository;
using Intern.NTQ.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Reponsitories.ProductImageReponsitories
{
    public class ProductImageRepository : RepositoryBase<ProductImg>, IProductImageRepository
    {
        private readonly NTQDbContext _dbContext;

        public ProductImageRepository(NTQDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ProductImg>> GetAllProduct(int? id)
        {
            var query = _dbContext.ProductImgs.Where(x=>x.ProductId==id).AsQueryable();
            var pageCount = query.Count();
            return query.ToList();
        }
    }
}
