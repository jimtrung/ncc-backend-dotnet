using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Api.DTOs;
using Theater_Management_BE.src.Application.Services;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpRequest request)
        {
            return Ok(await _userService.SignUp(request));
        }

        [HttpPost("signin")]
        public async Task<ActionResult<TokenPair>> SignIn([FromBody] SignInRequest request)
        {
            return Ok(await _userService.SignIn(request));
        }
    }
}
