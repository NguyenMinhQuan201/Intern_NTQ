using Intern.NTQ.Domain.Models.Authen;
using Intern.NTQ.Domain.Models.User;
using Intern.NTQ.Infrastructure.Models.Authen;
using Intern.NTQ.Infrastructure.Repositories.UserReponsitories;
using Intern.NTQ.Library.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Domain.IServices.User
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ApiResult<UserCreateRequest>> Create(UserCreateRequest request)
        {
            var obj = await _userRepository.GetUser(request.Email);
            if (obj != null)
            {
                return new ApiErrorResult<UserCreateRequest>("Tài khoản đã tồn tại");
            }
            var user = new Intern.NTQ.Infrastructure.Entities.User()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DeletedAt = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = request.UpdateAt,
                Role = request.role,
                PassWord = request.PassWord,
                Status = 1,
            };
            await _userRepository.CreateAsync(obj);
            return new ApiSuccessResult<UserCreateRequest>(request);
        }

        public async Task<ApiResult<LoginRespon>> Login(LoginRequest request)
        {
            var obj = await _userRepository.GetUser(request.Email);
            if (obj == null)
            {
                return new ApiErrorResult<LoginRespon>("Tài khoản hoặc mật khẩu sai");
            }
            var claims = new[]
            {
                new Claim("email",obj.Email),
                new Claim("role",obj.Role),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var getToken = new LoginRespon()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
            if (obj.PassWord == request.Password) return new ApiSuccessResult<LoginRespon>(getToken);
            return new ApiErrorResult<LoginRespon>("None");
        }

        public async Task<ApiResult<UserEditRequest>> Edit(int id, UserEditRequest request)
        {
            var obj = await _userRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<UserEditRequest>("Tài khoản đã tồn tại");
            }
            obj.Email = request.Email;
            obj.FirstName = request.FirstName;
            obj.LastName = request.LastName;
            obj.DeletedAt = null;
            obj.CreatedAt = request.CreateAt;
            obj.UpdatedAt = DateTime.Now;
            obj.Role = request.role;
            obj.PassWord = request.PassWord;
            obj.Status = request.Status;
            await _userRepository.UpdateAsync(obj);
            return new ApiSuccessResult<UserEditRequest>(request);
        }

        private string GenerateRefreshToken()
        {
            var random = new Byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var obj = await _userRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>("Không thành công");
            }
            obj.Status = 2;
            obj.DeletedAt = DateTime.Now;
            await _userRepository.UpdateAsync(obj);
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> UnRemove(int id)
        {
            var obj = await _userRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>("Không thành công");
            }
            obj.Status = 1;
            obj.DeletedAt = null;
            await _userRepository.UpdateAsync(obj);
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _userRepository.CountAsync();
            var query = await _userRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Intern.NTQ.Infrastructure.Entities.User, bool>> expression = x => x.Email.Contains(search);
                query = await _userRepository.GetAll(pageSize, pageIndex, expression);
                totalRow = await _userRepository.CountAsync(expression);
            }
            //Paging
            var data = query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .Select(x => new UserVm()
                {
                    Email = x.Email,
                    CreateAt = x.CreatedAt,
                    UpdateAt = x.UpdatedAt,
                    DeleteAt = x.DeletedAt,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName,
                    Status = x.Status,
                }).ToList();
            var pagedResult = new PagedResult<UserVm>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<UserVm>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);
        }

        public async Task<ApiResult<UserVm>> GetById(int id)
        {
            var obj = await _userRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<UserVm>("Không tồn tại");
            }
            var result = new UserVm()
            {
                Id = id,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                CreateAt = obj.CreatedAt,
                UpdateAt = obj.UpdatedAt,
                DeleteAt = obj.DeletedAt,
                Email = obj.Email,
                Status = obj.Status,
                PassWord = obj.PassWord,
            };
            return new ApiSuccessResult<UserVm>(result);    
        }
    }
}
