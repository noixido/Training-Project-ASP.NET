using Latihan.Models;
using Latihan.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : Controller
    {
        private UniversityRepository _universityRepository;

        public UniversityController(UniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpPost]
        public IActionResult addUniversity(University university)
        {
            var addUniv = _universityRepository.addUniversity(university);
            if (addUniv > 0)
            {
                var lastInserted = _universityRepository.GetLastInsertedData();
                if (lastInserted == null)
                {
                    return null;
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Inserted Successfully",
                    data = (object)lastInserted
                });
            }

            return BadRequest(new
            {
                status = StatusCodes.Status400BadRequest,
                message = "Data Cannot Be Inserted",
                data = (object)null
            });
        }

        [HttpGet]
        public IActionResult GetAllUniversities()
        {
            var getAllUniv = _universityRepository.GetAllUniversities();
            if (getAllUniv.Count() == 0)
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
                data = (object)getAllUniv
            });
        }

        [HttpGet("{univId}")]
        public IActionResult GetUniversityById(string univId)
        {
            var univ = _universityRepository.GetUniversityById(univId);
            if (univ == null)
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
                data = (object)univ
            });
        }

        [HttpPut("{univId}")]
        public IActionResult UpdateUniversity(string univId, [FromBody] University university)
        {
            var checkRecordId = _universityRepository.GetUniversityById(univId);
            if(checkRecordId == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Not Found",
                    data = (object)null
                });
            }

            checkRecordId.Univ_Name = university.Univ_Name;

            int res = _universityRepository.updateUniversity(checkRecordId);
            if(res > 0)
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

        [HttpDelete("{univId}")]
        public IActionResult DeleteUniversity(string? univId)
        {
            int res = _universityRepository.deleteUniversity(univId);
            if(res > 0)
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
