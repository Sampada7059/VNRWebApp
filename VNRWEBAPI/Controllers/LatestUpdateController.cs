using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNRWEBAPI.Repository;
using VNRWEBAPI.ViewModels;

namespace VNRWEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LatestUpdateController : ControllerBase
    {
        private readonly ILatestUpdateRepository _latestUpdateRepository;
        public LatestUpdateController(ILatestUpdateRepository latestUpdateRepository)
        {
            _latestUpdateRepository = latestUpdateRepository;
        }

        [HttpGet]
        public IActionResult GetAllUpdates()
        {
            var update = _latestUpdateRepository.GetAllUpdates();
            return Ok(update);
        }
        [HttpPost]
        public IActionResult AddLatestUpdate([FromBody] LatestUpdateVM latestUpdateVM)
        {
            try
            {
                _latestUpdateRepository.AddLatestUpdate(latestUpdateVM);
                return Ok(latestUpdateVM);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
