using Intern.NTQ.Infrastructure.Common.BaseRepository;
using Intern.NTQ.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Repositories.UserReponsitories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUser(string email,string pass);
        Task<User> GetUser(string email);
    }
}
