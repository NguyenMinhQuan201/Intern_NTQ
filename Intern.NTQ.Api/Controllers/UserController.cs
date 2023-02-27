using Domain.IServices.User;
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
        private readonly IUserService USERSERVICE;
        public UserController(IUserService userService)
        {
            USERSERVICE = userService;
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
                var resultToken = await USERSERVICE.Login(request);

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
                var resultToken = await USERSERVICE.Create(request);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
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
                var resultToken = await USERSERVICE.Edit(id,request);

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
                var resultToken = await USERSERVICE.Remove(id);

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
                var resultToken = await USERSERVICE.UnRemove(id);

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
                var resultToken = await USERSERVICE.GetAll(pageSize, pageIndex, search);

                if (resultToken.IsSuccessed == false)
                {
                    return BadRequest();
                }

                return Ok(resultToken);
            }
        }
    }
}
