using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Application.Services;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Application.Interfaces;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<User> GetUser()
        {
            var user = HttpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
                return Unauthorized("Chưa đăng nhập");

            var userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            User userInfo = _userService.GetUser(Guid.Parse(userId));

            return Ok(userInfo);
        }

        [HttpGet("all")]
        public ActionResult<int> GetAllUsersCount()
        {
            var users = _userService.GetAllUsers(); // vẫn lấy list
            int count = users?.Count ?? 0;           // lấy số lượng, tránh null
            return Ok(count);                        // trả về số nguyên
        }
    }
}
