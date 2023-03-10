using Intern.NTQ.Infrastructure.Models.Authen;
using Intern.NTQ.Manager.Services.Authen;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Intern.NTQ.Manager.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticateService _authenticateService;
        public LoginController(IConfiguration configuration, IAuthenticateService authenticateService)
        {
            _configuration = configuration;
            _authenticateService = authenticateService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = _configuration["Jwt:Issuer"];
            validationParameters.ValidIssuer = _configuration["Jwt:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            return principal;
        }
        //Login basic
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var LoginRepon = await _authenticateService.GetToken(request);
            if (LoginRepon.IsSuccessed == false)
            {
                ModelState.AddModelError("", LoginRepon.Message);
                return View();
            }
            var userPrincipal = this.ValidateToken(LoginRepon.ResultObj.Token);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true
            };
            HttpContext.Session.SetString("Token", LoginRepon.ResultObj.Token);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authProperties
                );
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(LoginRepon.ResultObj.Token);
            var role = jwtSecurityToken.Claims.First(claim => claim.Type == "role").Value;
            if (role == "user")
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Index", "Product");
        }
    }
}
