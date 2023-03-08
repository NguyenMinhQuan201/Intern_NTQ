using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ILogger _logger;
        public ValuesController(ILogger logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<string> Get()
        {
            _logger.LogInformation("PhucNgo");
            var strudent = "QuanDepZai";
            throw new Exception("Exception while getting the name");
        }
    }
}
