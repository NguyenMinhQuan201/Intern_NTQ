using Intern.NTQ.Domain.Models.Review;
using Intern.NTQ.Infrastructure.Repositories.ReviewRepository;
using Intern.NTQ.Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Features.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<ApiResult<ReviewCreateRequest>> Create(int id, ReviewCreateRequest request)
        {
            if (id == null)
            {
                return new ApiErrorResult<ReviewCreateRequest>("Không được null");
            }
            Expression<Func<Intern.NTQ.Infrastructure.Entities.Review, bool>> expression = x => x.UserId == id;
            var findById = await _reviewRepository.GetById(expression);
            if (request == null)
            {
                return new ApiErrorResult<ReviewCreateRequest>("Không được null");
            }
            var user = new Intern.NTQ.Infrastructure.Entities.Review()
            {
                DeletedAt = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Status = 1,
                Prize = request.Prize,
                Title = request.Title,
                UserId = id,
            };
            await _reviewRepository.CreateAsync(user);
            return new ApiSuccessResult<ReviewCreateRequest>(request);
        }

        public async Task<ApiResult<ReviewEditRequest>> Edit(int id, ReviewEditRequest request)
        {
            var obj = await _reviewRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<ReviewEditRequest>("Tài khoản đã tồn tại");
            }
            obj.Title = request.Title;
            obj.Prize = request.Prize;
            obj.UpdatedAt = DateTime.Now;
            obj.Status = request.Status;
            await _reviewRepository.UpdateAsync(obj);
            return new ApiSuccessResult<ReviewEditRequest>(request);
        }

        public async Task<ApiResult<PagedResult<ReviewVm>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _reviewRepository.CountAsync();
            var query = await _reviewRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Intern.NTQ.Infrastructure.Entities.Review, bool>> expression = x => x.Title.Contains(search);
                query = await _reviewRepository.GetAll(pageSize, pageIndex, expression);
                totalRow = await _reviewRepository.CountAsync(expression);
            }
            //Paging
            var data = query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .Select(x => new ReviewVm()
                {
                    Id = x.Id,
                    Status = x.Status,
                    DeletedAt = x.DeletedAt,
                    UpdatedAt = x.UpdatedAt,
                    CreatedAt = x.CreatedAt,
                    Prize= x.Prize,
                    Title= x.Title,
                }).ToList();
            var pagedResult = new PagedResult<ReviewVm>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ReviewVm>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ReviewVm>>(pagedResult);
        }

        public async Task<ApiResult<ReviewVm>> GetById(int id)
        {
            var obj = await _reviewRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<ReviewVm>("Không tồn tại");
            }
            var result = new ReviewVm()
            {
                Id = obj.Id,
                Status = obj.Status,
                DeletedAt = obj.DeletedAt,
                UpdatedAt = obj.UpdatedAt,
                CreatedAt = obj.CreatedAt,
                Prize = obj.Prize,
                Title = obj.Title,
            };
            return new ApiSuccessResult<ReviewVm>(result);
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var obj = await _reviewRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>("Không thành công");
            }
            obj.Status = 2;
            obj.DeletedAt = DateTime.Now;
            await _reviewRepository.UpdateAsync(obj);
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> UnRemove(int id)
        {
            var obj = await _reviewRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>("Không thành công");
            }
            obj.Status = 1;
            obj.DeletedAt = null;
            await _reviewRepository.UpdateAsync(obj);
            return new ApiSuccessResult<bool>();
        }
    }
}
