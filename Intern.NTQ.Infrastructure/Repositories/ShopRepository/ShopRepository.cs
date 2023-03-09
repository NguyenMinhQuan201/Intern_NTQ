using Infrastructure.EF;
using Intern.NTQ.Infrastructure.Common.BaseRepository;
using Intern.NTQ.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Repositories.ShopRepository
{
    public class ShopRepository : RepositoryBase<Shop>, IShopRepository
    {
        private readonly NTQDbContext _dbContext;
        public ShopRepository(NTQDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Shop>> GetFull()
        {
            var query = _dbContext.Shops.AsQueryable();
            return query.ToList();
        }
    }
}
