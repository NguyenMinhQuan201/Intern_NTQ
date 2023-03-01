using Intern.NTQ.Domain.Features;
using Intern.NTQ.Domain.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService=productService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _productService.Create(request);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(int id, [FromBody] ProductEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _productService.Edit(id, request);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _productService.Remove(id);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
        [HttpPut("unremove")]
        public async Task<IActionResult> UnRemove(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _productService.UnRemove(id);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
        [HttpGet("products")]
        public async Task<IActionResult> List(int? pageSize, int? pageIndex, string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _productService.GetAll(pageSize, pageIndex, search);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _productService.GetById(id);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
    }
}
