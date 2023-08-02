using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNRWEBAPI.Repository;
using VNRWEBAPI.ViewModels;

namespace VNRWEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginVM loginVM)
        {
            List<string>? loginValues = _usersRepository.Login(loginVM);
            
                if (loginValues != null)
                {
                    string nameOfUser = "", userName = "", userType = "", userId = "";
                    nameOfUser = loginValues[0];
                    userName = loginValues[1];
                    userType = loginValues[2];
                    userId = loginValues[3];
                    return Ok(new { nameOfUser, userName, userType, userId });
                }
                else
                {
                    return Ok("Invalid Username or Password");
                }
        }
    }
}
