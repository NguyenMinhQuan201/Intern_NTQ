using Intern.NTQ.Domain.Models.Product;
using Intern.NTQ.Library.Common;
using Microsoft.AspNetCore.Http;

namespace Intern.NTQ.Domain.Features
{
    public interface IProductService
    {
        public Task<ApiResult<ProductCreateRequest>> Create(ProductCreateRequest request);
        public Task<ApiResult<ProductEditRequest>> Edit(int id, ProductEditRequest request);
        public Task<ApiResult<bool>> Remove(int id);
        public Task<ApiResult<bool>> UnRemove(int id);
        public Task<ApiResult<PagedResult<ProductVm>>> GetAll(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<ProductVm>> GetById(int id);
        public Task<ApiResult<bool>> AddImage(int productId, List<IFormFile> request);
        public Task<int> RemoveImage(int imageId);
    }
}
