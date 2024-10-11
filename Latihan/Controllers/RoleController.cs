using Latihan.Models;
using Latihan.Repositories;
using Latihan.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly RoleRepository _roleRepository;
        public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public IActionResult addRole(Role? role)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(role.RoleName))
                {
                    return BadRequest(new
                    {
                        status = StatusCodes.Status400BadRequest,
                        message = "Role Name is required!",
                        data = (object)null
                    });
                }

                var addRole = _roleRepository.addRole(role);
                if (addRole == 0)
                {
                    return BadRequest(new
                    {
                        status = StatusCodes.Status400BadRequest,
                        message = "Failed to add new Role",
                        data = (object)null
                    });
                }
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "New Role successfully added",
                    data = (object) true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult getRoles()
        {
            try
            {
                var getRole = _roleRepository.GetAllRoles();
                if(getRole == null)
                {
                    return BadRequest(new
                    {
                        status = StatusCodes.Status400BadRequest,
                        message = "Data not found!",
                        data = (object)null
                    });
                }
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data found",
                    data = (object)getRole
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }

        [HttpGet("{roleId}")]
        public IActionResult GetRoleById(string roleId)
        {
            var role = _roleRepository.GetRoleById(roleId);
            if (role == null)
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
                data = (object)role
            });
        }

        [HttpPut("{roleId}")]
        public IActionResult UpdateRole(string roleId, [FromBody] Role role)
        {
            var checkRecordId = _roleRepository.GetRoleById(roleId);
            if (checkRecordId == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Not Found",
                    data = (object)null
                });
            }

            checkRecordId.RoleName = role.RoleName;

            int res = _roleRepository.updateRole(checkRecordId);
            if (res > 0)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Updated Successfully",
                    data = (object)checkRecordId
                });
            }

            return NotFound(new
            {
                status = StatusCodes.Status404NotFound,
                message = "Data Not Found",
                data = (object)null
            });
        }

        [HttpDelete("{roleId}")]
        public IActionResult DeleteRole(string? roleId)
        {
            int res = _roleRepository.deleteRole(roleId);
            if (res > 0)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Deleted Successfully",
                    data = (object)true
                });
            }
            return NotFound(new
            {
                status = StatusCodes.Status404NotFound,
                message = "Data Not Found",
                data = (object)null,
            });
        }
    }
}
