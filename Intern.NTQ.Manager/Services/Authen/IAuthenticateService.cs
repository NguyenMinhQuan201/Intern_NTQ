using Intern.NTQ.Domain.Models.Authen;
using Intern.NTQ.Infrastructure.Models.Authen;
using Intern.NTQ.Library.Common;
using Newtonsoft.Json;
using System.Text;

namespace Intern.NTQ.Manager.Services.Authen
{
    public interface IAuthenticateService
    {
        Task<ApiResult<LoginRespon>> GetToken(LoginRequest request);
    }
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticateService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<LoginRespon>> GetToken(LoginRequest rs)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(rs);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/User/login", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<ApiSuccessResult<LoginRespon>>(body);
            return loaisanpham;
        }
    }
}
