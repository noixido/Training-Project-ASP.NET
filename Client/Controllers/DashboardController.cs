using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class DashboardController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.data = "DashboardActiveClass";
            return View();
        }
    }
}
