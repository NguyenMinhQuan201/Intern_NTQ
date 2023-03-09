using Intern.NTQ.Domain.Features;
using Intern.NTQ.Domain.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
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
                var result = await _productService.Create(request);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
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
                var result = await _productService.Edit(id, request);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
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
                var result = await _productService.Remove(id);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        [HttpDelete("unremove")]
        public async Task<IActionResult> UnRemove(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _productService.UnRemove(id);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
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
                var result = await _productService.GetAll(pageSize, pageIndex, search);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
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
                var result = await _productService.GetById(id);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        [HttpDelete("remove-image")]
        public async Task<IActionResult> RemoveImage(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _productService.RemoveImage(id);

                if (result == 0)
                {
                    return BadRequest();
                }

                return Ok();
            }
        }
        [HttpPost("add-image")]
        public async Task<IActionResult> AddImage([FromForm]AddImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _productService.AddImage(request.Id,request.ProductImageVMs);

                if (result.IsSuccessed != true)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
        }

    }
}
