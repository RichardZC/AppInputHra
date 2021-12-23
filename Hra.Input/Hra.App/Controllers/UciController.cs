using Microsoft.AspNetCore.Mvc;

namespace Hra.App.Controllers
{
    public class UciController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
