using Microsoft.AspNetCore.Mvc;

namespace Hra.Input.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
