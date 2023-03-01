using Intern.NTQ.Domain.Models.Review;
using Intern.NTQ.Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Features
{
    public interface IReviewService
    {
        public Task<ApiResult<ReviewCreateRequest>> Create(int id, ReviewCreateRequest request);
        public Task<ApiResult<ReviewEditRequest>> Edit(int id, ReviewEditRequest request);
        public Task<ApiResult<bool>> Remove(int id);
        public Task<ApiResult<bool>> UnRemove(int id);
        public Task<ApiResult<PagedResult<ReviewVm>>> GetAll(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<ReviewVm>> GetById(int id);
    }
}
