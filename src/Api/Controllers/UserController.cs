using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Application.Services;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            var user = HttpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
                return Unauthorized("You must log in first, lil ape 🍌🚬");

            var userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            User userInfo = await _userService.GetUserAsync(Guid.Parse(userId));

            return Ok(userInfo);
        }
    }
}
