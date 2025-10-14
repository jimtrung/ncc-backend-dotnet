using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Api.DTOs;
using Theater_Management_BE.src.Application.Services;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpRequest request)
        {
            User user = new User();
            user.Username = request.Username;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Password = request.Password;

            return Ok(await _userService.SignUp(user));
        }
    }
}
