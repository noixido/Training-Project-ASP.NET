using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.data = "RegisterActiveClass";
            return View();
        }
    }
}
