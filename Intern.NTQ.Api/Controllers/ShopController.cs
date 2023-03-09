using Intern.NTQ.Domain.Features;
using Intern.NTQ.Domain.Models.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] ShopCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _shopService.Create(request);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(int id, [FromBody] ShopEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _shopService.Edit(id, request);

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
                var result = await _shopService.Remove(id);

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
                var result = await _shopService.UnRemove(id);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        [HttpGet("shops")]
        public async Task<IActionResult> List(int? pageSize, int? pageIndex, string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _shopService.GetAll(pageSize, pageIndex, search);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        [HttpGet("shops-full")]
        public async Task<IActionResult> List()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _shopService.GetAll();

                if (result.IsSuccessed == false && result.ResultObj.Count>0)
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
                var result = await _shopService.GetById(id);

                if (result.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
        }
    }
}
