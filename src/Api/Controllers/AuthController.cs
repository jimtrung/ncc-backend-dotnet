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
        public ActionResult<User> SignUp([FromBody] SignUpRequest request)
        {
            var user = _userService.SignUp(request);
            return Ok(user);
        }

        [HttpPost("signin")]
        public ActionResult<TokenPair> SignIn([FromBody] SignInRequest request)
        {
            var tokenPair = _userService.SignIn(request);
            Console.WriteLine(tokenPair.ToString());
            return Ok(tokenPair);
        }

        [HttpPost("refresh")]
        public ActionResult<string> Refresh([FromBody] RefreshRequest request)
        {
            string newAccessToken = _userService.Refresh(request.RefreshToken);
            if (newAccessToken == null) return Unauthorized("Expired refresh token");
            return Ok(newAccessToken);
        }
    }
}
