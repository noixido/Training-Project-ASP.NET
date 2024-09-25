using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ForgetPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
