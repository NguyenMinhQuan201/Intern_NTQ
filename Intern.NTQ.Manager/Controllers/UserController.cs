using Microsoft.AspNetCore.Mvc;

namespace Intern.NTQ.Manager.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
