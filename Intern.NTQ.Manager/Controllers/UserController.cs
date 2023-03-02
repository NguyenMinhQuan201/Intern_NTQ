using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Manager.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
