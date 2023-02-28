using Intern.NTQ.Domain.Models.Authen;
using Intern.NTQ.Domain.Models.User;
using Intern.NTQ.Infrastructure.Models.Authen;
using Intern.NTQ.Library.Common;
using System.Security.Claims;

namespace Domain.IServices.User
{
    public interface IUserService
    {
        public Task<ApiResult<LoginRespon>> Login(LoginRequest request);
        public Task<ApiResult<UserCreateRequest>> Create(UserCreateRequest request);
        public Task<ApiResult<UserEditRequest>> Edit( int id, UserEditRequest request);
        public Task<ApiResult<bool>> Remove(int id);
        public Task<ApiResult<bool>> UnRemove(int id);
        public Task<ApiResult<PagedResult<UserVm>>> GetAll(int?pageSize, int?pageIndex, string?search);
        public Task<ApiResult<UserVm>> GetById(int id);
    }
}
