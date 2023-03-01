using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace Intern.NTQ.Manager.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessions = context.HttpContext.Session.GetString("Token");
            if (sessions == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(sessions);
            var TenHienThi = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;
            ViewBag.TenHienThi = TenHienThi;
            base.OnActionExecuting(context);
        }
    }
}
