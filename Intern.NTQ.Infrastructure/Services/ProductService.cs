using AutoMapper;
using Domain.Common.FileStorage;
using Intern.NTQ.Domain.Features;
using Intern.NTQ.Domain.Models.Product;
using Intern.NTQ.Infrastructure.Entities;
using Intern.NTQ.Infrastructure.Reponsitories.ProductImageReponsitories;
using Intern.NTQ.Infrastructure.Repositories.ProductRepository;
using Intern.NTQ.Library.Common;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace Intern.NTQ.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IStorageService _storageService;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;
        public ProductService(IStorageService storageService, IProductRepository productRepository , IProductImageRepository productImageRepository,IMapper mapper)
        {
            _storageService = storageService;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<bool>> AddImage(int productId, List<IFormFile> request)
        {
            if (productId != null)
            {
                var findobj = await _productRepository.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var temp = new List<ProductImg>();
                if (request != null)
                {
                    foreach (var img in request)
                    {
                        var _productImage = new ProductImg()
                        {
                            Caption = findobj.Name,
                            DateCreated = DateTime.Now,
                            FileSize = img.Length,
                            ImagePath = await this.SaveFile(img),
                            IsDefault = true,
                        };
                        temp.Add(_productImage);
                    }
                    findobj.ProductImgs = temp;
                }
                await _productRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<ProductCreateRequest>> Create(ProductCreateRequest request)
        {
            if (request==null)
            {
                return new ApiErrorResult<ProductCreateRequest>("null");
            }
            var obj = new Product()
            {
                Name = request.Name,
                Slug = request.Slug,
                Price = request.Price,
                ProductDetail = request.ProductDetail,
                Trending = request.Trending,
                CreatedAt = DateTime.Now,
                Status=1,
                ShopId=request.ShopId
            };
            var temp = new List<ProductImg>();
            if (request != null)
            {
                foreach (var img in request.Images)
                {
                    var _productImage = new ProductImg()
                    {
                        Caption = obj.Name,
                        DateCreated = DateTime.Now,
                        FileSize = img.Length,
                        ImagePath = await this.SaveFile(img),
                        IsDefault = true,
                    };
                    temp.Add(_productImage);
                }
                obj.ProductImgs = temp;
            }
            await _productRepository.CreateAsync(obj);
            return new ApiSuccessResult<ProductCreateRequest>(request);
        }

        public async Task<ApiResult<ProductEditRequest>> Edit(int id, ProductEditRequest request)
        {
            var obj = await _productRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<ProductEditRequest>("Tài khoản không tồn tại");
            }
            obj.ProductDetail = request.ProductDetail;
            obj.Slug = request.Slug;
            obj.Trending = request.Trending;
            obj.Name = request.Name;
            obj.Price = request.Price;
            obj.UpdatedAt= DateTime.Now;
            obj.ShopId=request.ShopId;
            await _productRepository.UpdateAsync(obj);
            return new ApiSuccessResult<ProductEditRequest>(request);
        }

        public async Task<ApiResult<PagedResult<ProductVm>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _productRepository.CountAsync();
            var query = await _productRepository.GetAllProduct(pageSize, pageIndex);
            Expression<Func<Product, ICollection<ProductImg>>> expressionInclude = x => x.ProductImgs;
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Product, bool>> expression = x => x.Name.Contains(search);
                query = await _productRepository.GetAllProduct(pageSize, pageIndex, expression);
                totalRow = await _productRepository.CountAsync(expression);
            }
            var data = query
                .Select(x => new ProductVm()
                {
                    Name = x.Name,
                    Price = x.Price,
                    ProductDetail = x.ProductDetail,
                    Slug = x.Slug,
                    Trending = x.Trending,
                    Id = x.Id,
                    /*ProductImageVMs = x.ProductImgs,*/
                    ProductImageVMs = _mapper.Map<List<ProductImageVM>>(x.ProductImgs),
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt=x.UpdatedAt,
                    DeletedAt=x.DeletedAt,
                    
                }).OrderByDescending(x=>x.Trending).ToList();
            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ProductVm>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ProductVm>>(pagedResult);
        }

        public async Task<ApiResult<ProductVm>> GetById(int id)
        {
            var findobj = await _productRepository.GetByProductID(id);
            if (findobj == null)
            {
                return null;
            }

            var obj = new ProductVm()
            {
                Name = findobj.Name,
                Price = findobj.Price,
                ProductDetail = findobj.ProductDetail,
                Slug = findobj.Slug,
                Trending = findobj.Trending,
                Id = id,
                ProductImageVMs = _mapper.Map<List<ProductImageVM>>(findobj.ProductImgs),
                Status =findobj.Status,
                CreatedAt = findobj.CreatedAt,
                UpdatedAt = findobj.UpdatedAt,
                DeletedAt = findobj.DeletedAt,
            };
            return new ApiSuccessResult<ProductVm>(obj);
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var obj = await _productRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            obj.DeletedAt = DateTime.Now;
            obj.Status = 2;
            await _productRepository.UpdateAsync(obj);
            return new ApiSuccessResult<bool>();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _productImageRepository.GetById(imageId);
            await _storageService.DeleteFileAsync(productImage.ImagePath);
            if (productImage == null)
            {
                return 0;
            }
            await _productImageRepository.DeleteAsync(productImage);
            return 1;
        }

        public async Task<ApiResult<bool>> UnRemove(int id)
        {
            var obj = await _productRepository.GetById(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            obj.DeletedAt = null;
            obj.Status = 1;
            await _productRepository.UpdateAsync(obj);
            return new ApiSuccessResult<bool>();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
