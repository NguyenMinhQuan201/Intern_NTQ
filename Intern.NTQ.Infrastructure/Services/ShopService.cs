using Intern.NTQ.Domain.Features;
using Intern.NTQ.Domain.Models.Shop;
using Intern.NTQ.Infrastructure.Repositories.ShopRepository;
using Intern.NTQ.Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository=shopRepository;
        }
        public async Task<ApiResult<ShopCreateRequest>> Create(ShopCreateRequest request)
        {
            if (request == null)
            {
                return new ApiErrorResult<ShopCreateRequest>("Không được null");
            }
            var user = new Intern.NTQ.Infrastructure.Entities.Shop()
            {
                Name = request.Name,
                CreatedAt = DateTime.Now,
                Status = 1
            };
            await _shopRepository.CreateAsync(user);
            return new ApiSuccessResult<ShopCreateRequest>(request);
        }

        public async Task<ApiResult<ShopEditRequest>> Edit(int id, ShopEditRequest request)
        {
            var obj = await _shopRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<ShopEditRequest>("Tài khoản không tồn tại");
            }
            obj.Name = request.Name;
            obj.UpdatedAt = DateTime.Now;
            await _shopRepository.UpdateAsync(obj);
            return new ApiSuccessResult<ShopEditRequest>(request);
        }

        public async Task<ApiResult<PagedResult<ShopVm>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _shopRepository.CountAsync();
            var query = await _shopRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Intern.NTQ.Infrastructure.Entities.Shop, bool>> expression = x => x.Name.Contains(search);
                query = await _shopRepository.GetAll(pageSize, pageIndex, expression);
                totalRow = await _shopRepository.CountAsync(expression);
            }
            //Paging
            var data = query
                .Select(x => new ShopVm()
                {
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    Status = x.Status,
                }).ToList();
            var pagedResult = new PagedResult<ShopVm>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ShopVm>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ShopVm>>(pagedResult);
        }

        public async Task<ApiResult<List<ShopVm>>> GetAll()
        {
            var query = await _shopRepository.GetFull();
            var data = query
                .Select(x => new ShopVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    Status = x.Status,
                }).ToList();
            return new ApiSuccessResult<List<ShopVm>>(data);
        }

        public async Task<ApiResult<ShopVm>> GetById(int id)
        {
            var obj = await _shopRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<ShopVm>("Không tồn tại");
            }
            var result = new ShopVm()
            {
                Id = id,
                Name = obj.Name,
                CreatedAt = obj.CreatedAt,
                UpdatedAt = obj.UpdatedAt,
                DeletedAt = obj.DeletedAt,
                Status = obj.Status,
            };
            return new ApiSuccessResult<ShopVm>(result);
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var obj = await _shopRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            obj.DeletedAt = DateTime.Now;
            obj.Status = 2;
            await _shopRepository.UpdateAsync(obj);
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> UnRemove(int id)
        {
            var obj = await _shopRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            obj.DeletedAt = null;
            obj.Status = 1;
            await _shopRepository.UpdateAsync(obj);
            return new ApiSuccessResult<bool>();
        }
    }
}
