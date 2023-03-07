using Intern.NTQ.Domain.Models.Product;
using Intern.NTQ.Library.Common;
using Intern.NTQ.Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Intern.NTQ.Manager.Services.Product
{
    public interface IProductService
    {
        Task<ApiResult<bool>> Create(Models.ProductCreateRequest request);
        Task<ApiResult<Models.ProductEditRequest>> Edit(Models.ProductEditRequest request);
        Task<ApiResult<bool>> Remove(int id);
        Task<ApiResult<bool>> UnRemove(int id);
        Task<ApiResult<ProductViewModel>> GetByCondition(int id);
        Task<ApiResult<PagedResult<ProductViewModel>>> GetAll(int? pageSize, int? pageIndex, string? search);
    }
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<bool>> Create(Models.ProductCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var requestContent = new MultipartFormDataContent();
            if (request.Images != null)
            {
                foreach (var file in request.Images)
                {

                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)file.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);
                    requestContent.Add(bytes, "images", file.FileName.ToString());
                }

            }
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.ProductDetail.ToString()), "productDetail");
            requestContent.Add(new StringContent(request.ProductDetail.ToString()), "productDetail");
            requestContent.Add(new StringContent(request.Slug.ToString()), "slug");
            requestContent.Add(new StringContent(request.Trending.ToString()), "trending");
            var response = await client.PostAsync($"/api/Product/create", requestContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>("loi");
        }

        public async Task<ApiResult<Models.ProductEditRequest>> Edit(Models.ProductEditRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Product/edit?id=" + request.Id.ToString(), httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<Models.ProductEditRequest>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<Models.ProductEditRequest>>("loi");
        }

        public async Task<ApiResult<PagedResult<ProductViewModel>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Product/products?pageSize={pageSize}&pageIndex={pageIndex}&search={search}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<ProductViewModel>>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<ProductViewModel>>>("loi");
        }

        public async Task<ApiResult<ProductViewModel>> GetByCondition(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Product/getbyid?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<ProductViewModel>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<ProductViewModel>>(body);
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/Product/remove?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        public async Task<ApiResult<bool>> UnRemove(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/Product/unremove?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }
    }
}
