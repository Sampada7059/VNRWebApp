using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNRWEBAPI.Repository;
using VNRWEBAPI.ViewModels;

namespace VNRWEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentVM studentVM)
        {
            try
            {
                _studentRepository.AddStudent(studentVM);
                return Ok(studentVM);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
