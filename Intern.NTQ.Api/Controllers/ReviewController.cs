using Intern.NTQ.Domain.Features;
using Intern.NTQ.Domain.Models.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(int id, [FromForm] ReviewCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _reviewService.Create(id ,request);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(int id, [FromBody] ReviewEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _reviewService.Edit(id, request);

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
                var resultToken = await _reviewService.Remove(id);

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
                var resultToken = await _reviewService.UnRemove(id);

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
                var resultToken = await _reviewService.GetAll(pageSize, pageIndex, search);

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
                var resultToken = await _reviewService.GetById(id);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
    }
}
