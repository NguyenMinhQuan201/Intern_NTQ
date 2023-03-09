using Intern.NTQ.Domain.Models.Shop;
using Intern.NTQ.Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Features
{
    public interface IShopService
    {
        public Task<ApiResult<ShopCreateRequest>> Create(ShopCreateRequest request);
        public Task<ApiResult<ShopEditRequest>> Edit(int id, ShopEditRequest request);
        public Task<ApiResult<bool>> Remove(int id);
        public Task<ApiResult<bool>> UnRemove(int id);
        public Task<ApiResult<PagedResult<ShopVm>>> GetAll(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<List<ShopVm>>> GetAll();
        public Task<ApiResult<ShopVm>> GetById(int id);
    }
}
