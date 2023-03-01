using Infrastructure.EF;
using Intern.NTQ.Infrastructure.Common.BaseRepository;
using Intern.NTQ.Infrastructure.Entities;
using Intern.NTQ.Infrastructure.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Repositories.ReviewRepository
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        private readonly NTQDbContext _dbContext;
        public ReviewRepository(NTQDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
