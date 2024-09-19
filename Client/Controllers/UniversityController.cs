using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UniversityController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.data = "UniversityActiveClass";
            return View();
        }
    }
}
