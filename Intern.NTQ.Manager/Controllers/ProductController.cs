using Intern.NTQ.Domain.Models.Product;
using Intern.NTQ.Library.Common;
using Intern.NTQ.Manager.Models;
using Intern.NTQ.Manager.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Manager.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ActionResult> Index(int? page, string? search, string? option)
        {
            if (page == null) page = 1;
            var result = await _productService.GetAll(15, page, search);
            ViewBag.Search = search;
            if (option == "Trending")
            {
                ViewBag.Option = option;
                var temp = new PagedResult<ProductViewModel>()
                {
                    PageSize = result.ResultObj.PageSize,
                    PageIndex = result.ResultObj.PageIndex,
                    TotalRecord = result.ResultObj.TotalRecord,
                    Items = result.ResultObj.Items.Where(x => x.Trending == true).ToList(),
                };
                return View(temp);
            }
            if (option == "Removed")
            {
                ViewBag.Option = option;
                PagedResult<ProductViewModel> temp = new PagedResult<ProductViewModel>()
                {
                    PageSize = result.ResultObj.PageSize,
                    PageIndex = result.ResultObj.PageIndex,
                    TotalRecord = result.ResultObj.TotalRecord,
                    Items = result.ResultObj.Items.Where(x => x.Status == 2).ToList()
                };
                return View(temp);
            }
            if (option == "Default" || option == null)
            {
                ViewBag.Search = "";
                var temp = new PagedResult<ProductViewModel>()
                {
                    PageSize = result.ResultObj.PageSize,
                    PageIndex = result.ResultObj.PageIndex,
                    TotalRecord = result.ResultObj.TotalRecord,
                    Items = result.ResultObj.Items.Where(x => x.Status == 1).ToList(),
                };
                return View(temp);
            }
            return View(result.ResultObj);
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.Remove(id);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Index", "Product");
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
                var result = await _productService.UnRemove(id);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Index", "Product");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] Models.ProductCreateRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.Create(request);
                    if (result.IsSuccessed == true)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Index", "Product");
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _productService.GetByCondition(id);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Product");
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
        public async Task<IActionResult> Edit(Models.ProductEditRequest request)
        {
            try
            {
                var result = await _productService.Edit(request);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Index", "Product");
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _productService.GetByCondition(id);
                if (result.IsSuccessed == false)
                {
                    return RedirectToAction("Index", "Product");
                }
                return View(result.ResultObj);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddMoreImage(Models.AddImageRequest request)
        {
            try
            {
                var result = await _productService.AddImage( request);
                if (result.IsSuccessed == true)
                {
                    return RedirectToAction("Details", "Product", new { id = request.Id });
                }
                return View(result.ResultObj);
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> RemoveImage(int id ,int idProduct)
        {
            try
            {
                var result = await _productService.DeleteImage(id);
                if (result==1)
                {
                    return RedirectToAction("Details", "Product", new { id = idProduct });
                }
                return RedirectToAction("Details", "Product", new { id = idProduct });
            }
            catch
            {
                return View();
            }
        }
    }
}
