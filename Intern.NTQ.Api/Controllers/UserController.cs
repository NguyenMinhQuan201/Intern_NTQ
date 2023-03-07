using Intern.NTQ.Domain.Features;
using Intern.NTQ.Domain.Models.Authen;
using Intern.NTQ.Domain.Models.User;
using Intern.NTQ.Infrastructure.Models.Authen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _userService.Login(request);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest(resultToken);
                }

                return Ok(resultToken);
            }
        }
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _userService.Create(request);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest(resultToken.Message);
                }

                return Ok(resultToken);
            }
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit( int id, [FromBody] UserEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _userService.Edit(id,request);

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
                var resultToken = await _userService.Remove(id);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
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
                var resultToken = await _userService.UnRemove(id);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
        [HttpGet("users")]
        public async Task<IActionResult> ListUser(int? pageSize, int? pageIndex, string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _userService.GetAll(pageSize, pageIndex, search);

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
                var resultToken = await _userService.GetById(id);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
        [HttpGet("getbyemail")]
        public async Task<IActionResult> GetByUserName(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var resultToken = await _userService.GetByUserName(email);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
    }
}
