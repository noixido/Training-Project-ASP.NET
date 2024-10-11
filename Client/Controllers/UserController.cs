using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        [Route("employee")]
        public IActionResult Index()
        {
            ViewBag.data = "EmployeeActiveClass";
            return View();
        }
    }
}
