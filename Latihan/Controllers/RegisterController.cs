using Latihan.Models;
using Latihan.Repositories;
using Latihan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class RegisterController : Controller
    {
        private readonly RegisterRepository _repository;
        private readonly IConfiguration _configuration;

        public RegisterController(RegisterRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Register(RegisterVM? registerVM)
        {
            try
            {
                var register = _repository.Register(registerVM);
                if (register > 0)
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
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllEmployeeData()
        {
            var GetAllEmpData = _repository.GetAllEmpData();
            if (GetAllEmpData == null)
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
        public IActionResult Login(LoginVM loginVM)
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

                var payload = _repository.GetPayload(loginVM.Username);
                payload.Username = loginVM.Username;
                var claims = new List<Claim>
                {
                    new Claim("Username", payload.Username),
                    new Claim("fullName", payload.FullName),
                    new Claim("role", payload.Roles),
                    //new Claim(ClaimTypes.Role, payload.Roles),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:API"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signIn
                );
                var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Login Success!",
                    data = (object)tokenResult,
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

        [HttpGet("countDegree")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCountDegree()
        {
            var countDegree = _repository.GetCountDegree();
            if (countDegree.Count() == 0)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Not Found",
                    data = (object)countDegree
                });
            }

            var data = countDegree.ToDictionary(n => n.Degree, n => n.Count);
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Data Found",
                data = (object)data
            });
        }

        [HttpGet("getByEmail/{email}")]
        public IActionResult GetEmpByEmail(string email)
        {
            var acc = _repository.GetEmpByEmail(email);
            if (acc == null)
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
                data = (object)acc
            });

        }

        [HttpGet("getEmployeeByEmail/{email}")]
        public IActionResult GetEmployeeByEmail(string email)
        {
            var acc = _repository.GetEmployeeByEmail(email);
            if (acc == null)
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
                data = (object)acc
            });

        }

        [HttpPut("editProfile/{email}")]
        public IActionResult updateProfile(string email, UpdateProfileVM? updateProfileVM)
        {
            try
            {
                if(email != updateProfileVM.Email)
                {
                    return BadRequest(new
                    {
                        status = StatusCodes.Status400BadRequest,
                        message = "email is not correct",
                    });
                }
                int data = _repository.updateEmployee(updateProfileVM);
                if(data > 0)
                {
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "Data updated!",
                        data = (object) data
                    });
                }
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Not Found",
                    data = (object)null
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

        //[Authorize]
        [HttpPut("changePassword")]
        public IActionResult changePassword(ChangePassVM changePassVM)
        {
            try
            {
                //if(email != changePassVM.Email)
                //{
                //    return BadRequest(new
                //    {
                //        status = StatusCodes.Status400BadRequest,
                //        message = "Email is not correct",
                //    });
                //}

                int data = _repository.changePassword(changePassVM);
                if (data > 0)
                {
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "Data updated!",
                        data = (object) true
                    });
                }
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Not Found",
                    data = (object) null
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
