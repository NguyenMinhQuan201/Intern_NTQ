using Intern.NTQ.Domain.Models.User;
using Intern.NTQ.Manager.Models;
using Intern.NTQ.Manager.Services.Authen;
using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Manager.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAdminService _adminService;
        public UserController(IAdminService adminService)
        {
            _adminService=adminService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string email)
        {
            var result = await _adminService.GetByCondition(email);
            if (result.IsSuccessed == false)
            {
                return RedirectToAction("Index", "User");
            }
            return View(result.ResultObj);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserEditViewModel request)
        {
            var result = await _adminService.Edit(request);
            if (result.IsSuccessed == false)
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Profile", "User");
        }

    }
}
