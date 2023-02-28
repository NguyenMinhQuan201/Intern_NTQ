using Infrastructure.EF;
using Intern.NTQ.Infrastructure.Common.BaseRepository;
using Intern.NTQ.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly NTQDbContext _dbContext;
        public ProductRepository(NTQDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex)
        {
            var query = _dbContext.Products.Include(x => x.ProductImgs).AsQueryable();
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();

        }

        public async Task<List<Product>> GetAllProduct(int? pageSize, int? pageIndex, Expression<Func<Product, bool>> expression)
        {
            var query = _dbContext.Products.Where(expression).Include(x => x.ProductImgs).AsQueryable();
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();
        }

        public async Task<Product> GetByProductID(int? id)
        {
            var query = _dbContext.Products.Include(x => x.ProductImgs).Where(x => x.Id == id).FirstOrDefault();
            return query;
        }
    }
}
