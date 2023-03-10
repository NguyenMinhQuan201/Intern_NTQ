using Intern.NTQ.Domain.Models.User;
using Intern.NTQ.Library.Common;
using Intern.NTQ.Manager.Models;
using Intern.NTQ.Manager.Services.Authen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Intern.NTQ.Manager.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string email)
        {
            var result = await _adminService.GetByCondition(email);
            if (result.IsSuccessed == false)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(result.ResultObj);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserEditViewModel request)
        {
            var result = await _adminService.Edit(request);
            if (result.IsSuccessed == false)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Profile", "Admin");
        }
        // GET: AdminController
        public async Task<ActionResult> Index(int? page, string? search, string? option)

        {
            if (page == null) page = 1;
            var result = await _adminService.GetAll(10, page, search);
            if (option == "Admin" || ViewBag.Option == "Admin")
            {
                ViewBag.Option = option;
                var temp = new PagedResult<UserViewModel>()
                {
                    PageSize = result.ResultObj.PageSize,
                    PageIndex = result.ResultObj.PageIndex,
                    TotalRecord = result.ResultObj.TotalRecord,
                    Items = result.ResultObj.Items.Where(x => x.Role == "admin").ToList(),
                };
                return View(temp);
            }
            if (option == "Removed"|| ViewBag.Option== "Removed")
            {
                ViewBag.Option = option;
                PagedResult<UserViewModel> temp = new PagedResult<UserViewModel>()
                {
                    PageSize = result.ResultObj.PageSize,
                    PageIndex = result.ResultObj.PageIndex,
                    TotalRecord = result.ResultObj.TotalRecord,
                    Items = result.ResultObj.Items.Where(x => x.Status == 2).ToList()
                };
                return View(temp);
            }
            if (option == "Default"||option==null)
            {
                ViewBag.Search = "";
                var temp = new PagedResult<UserViewModel>()
                {
                    PageSize = result.ResultObj.PageSize,
                    PageIndex = result.ResultObj.PageIndex,
                    TotalRecord = result.ResultObj.TotalRecord,
                    Items = result.ResultObj.Items.Where(x=>x.Status==1).ToList(),
                    /*Items = result.ResultObj.Items,*/
                };
                return View(temp);
            }
            ViewBag.Search = search;
            ViewBag.Option = option;
            
            return View(result.ResultObj);
        }
        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _adminService.Create(request);
                    return RedirectToAction("Index", "Admin");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public async Task<IActionResult> Edit(string email)
        {
            try
            {
                var result = await _adminService.GetByCondition(email);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Admin");
                }
                return View(result.ResultObj);
            }
            catch
            {
                return View();
            }
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel request)
        {
            try
            {
                var result = await _adminService.Edit(request);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _adminService.Remove(id);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> UnRemove(int id)
        {
            try
            {
                var result = await _adminService.UnRemove(id);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return View();
            }
        }
    }
}
