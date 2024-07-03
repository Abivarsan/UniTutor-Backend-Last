// Controllers/UserController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniTutor.Interface;
using UniTutor.Respository;

namespace UniTutor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userRepository;

        public UsersController(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("last-joined")]
        public async Task<IActionResult> GetLastJoinedUsers([FromQuery] int count = 10)
        {
            var users = await _userRepository.GetLastJoinedUsersAsync(count);
            return Ok(users);
        }
    }
}
