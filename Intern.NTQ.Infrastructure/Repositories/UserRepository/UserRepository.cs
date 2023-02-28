using Infrastructure.EF;
using Intern.NTQ.Infrastructure.Common.BaseRepository;
using Intern.NTQ.Infrastructure.Entities;
using Intern.NTQ.Infrastructure.Repositories.UserReponsitories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Repositories.UserRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly NTQDbContext _dbContext;
        public UserRepository(NTQDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<User> IUserRepository.GetUser(string email)
        {
            var user = await _dbContext.Users.Where(x=>x.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}
