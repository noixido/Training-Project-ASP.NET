using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.data = "RoleActiveClass";
            return View();
        }
    }
}
