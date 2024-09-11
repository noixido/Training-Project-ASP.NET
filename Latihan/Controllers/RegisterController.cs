using Latihan.Repositories;
using Latihan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private RegisterRepository _repository;

        public RegisterController(RegisterRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Register(RegisterVM? registerVM)
        {
            try
            {
                var register = _repository.Register(registerVM);
                if(register > 0)
                {
                    var data = _repository.lastInsertedEmpData();
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "Data Registered Successfully!",
                        data = (object)data
                    });
                }

                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Data Cannot be Registered!",
                    data = (object)null,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                });
            }
        }

        [HttpGet]
        public IActionResult GetAllEmployeeData()
        {
            var GetAllEmpData = _repository.GetAllEmpData();
            if(GetAllEmpData == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Not Found",
                    data = (object)null
                });
            }
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Data Found",
                data = (object)GetAllEmpData
            });
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginVM  loginVM)
        {
            try
            {
                var user = _repository.Login(loginVM);
                if (!user)
                {
                    return BadRequest(new
                    {
                        status = StatusCodes.Status400BadRequest,
                        message = "Login Failed!",
                        data = (object)false,
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Login Success!",
                    data = (object)user,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                });
            }
        }
    }
}
