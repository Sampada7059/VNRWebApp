using Microsoft.AspNetCore.Mvc;
using VNRWEBAPI.Repository;
using VNRWEBAPI.Models;
using VNRWEBAPI.ViewModels;

namespace YourProjectName.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        [HttpPost]
        public IActionResult CreateTeacher([FromBody] TeacherVM teacher)
        {
            try
            {
                _teacherRepository.AddTeacher(teacher);
                return Ok(teacher);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
