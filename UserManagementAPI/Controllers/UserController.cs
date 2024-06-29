using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Application;
using UserManagementAPI.Domain;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Endpoint to create a new user
        [HttpPost("create")]
        public ActionResult<User> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = _userService.CreateUser(createUserRequest);
            return Ok(newUser);
        }

        // Endpoint to get a user by email
        [HttpGet("email/{email}")]
        public ActionResult<User> GetUserByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Endpoint to get a user by phone number
        [HttpGet("phone/{phoneNumber}")]
        public ActionResult<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = _userService.GetUserByPhoneNumber(phoneNumber);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
