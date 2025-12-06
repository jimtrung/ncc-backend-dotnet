using System.Diagnostics;
using Theater_Management_BE.src.Api.DTOs;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Domain.Exceptions.User;
using Theater_Management_BE.src.Infrastructure.Utils;

namespace Theater_Management_BE.src.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        private readonly AuthTokenUtil _authTokenUtil;
        private readonly EmailValidator _emailValidator;

        public UserService(IUserRepository repo, AuthTokenUtil authTokenUtil, EmailValidator emailValidator)
        {
            _repo = repo;
            _authTokenUtil = authTokenUtil;
            _emailValidator = emailValidator;
        }

        public User SignUp(SignUpRequest request)
        {
            // 1. Kiểm tra rỗng và định dạng email
            if (request.Username == null) throw new InvalidUserDataException("Tên đăng nhập không được để trống");
            if (request.Email == null) throw new InvalidUserDataException("Email không được để trống");
            if (request.Password == null) throw new InvalidUserDataException("Mật khẩu không được để trống");
            if (!_emailValidator.IsValidEmail(request.Email))
                throw new InvalidUserDataException("Định dạng email không hợp lệ");
            
            // 2. Kiểm tra nếu tên người dùng hoặc email đã tồn tại
            var existingUserByUsername = _repo.GetByUsername(request.Username);
            if (existingUserByUsername != null) 
                throw new UserAlreadyExistsException("Tên đăng nhập đã tồn tại");
            
            var existingUserByEmail = _repo.GetByEmail(request.Email);
            if (existingUserByEmail != null) 
                throw new UserAlreadyExistsException("Email đã được sử dụng");

            // 3. Tạo token (xác thực email) và hash mật khẩu
            string token = TokenUtil.GenerateToken();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 4. Tạo user object
            User user = new()
            {
                Username = request.Username,
                Email = request.Email,
                Password = passwordHash,
                Token = token,
                Role = UserRole.user,
                Provider = Provider.local,
                Verified = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // 5. Gửi email xác thực (async - không chặn request)
            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Run(() => _emailValidator.SendVerificationEmail(user.Email, $"http://localhost:8080/auth/verify-email/{token}"));
                    Console.WriteLine("[DEBUG] Email đã được gửi thành công");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[DEBUG] Gửi email thất bại: " + ex.Message);
                }
            });

            // 6. Lưu vào CSDL
            var savedUser = _repo.Add(user);

            return savedUser;
        }

        public TokenPair SignIn(SignInRequest request)
        {
            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new InvalidUserDataException("Tên đăng nhập không được để trống");
            if (string.IsNullOrWhiteSpace(request.Password))
                throw new InvalidUserDataException("Mật khẩu không được để trống");

            // 2. Kiểm tra user tồn tại
            var user = _repo.GetByUsername(request.Username)
                       ?? throw new UserNotFoundException("Tên đăng nhập không tồn tại");

            // 3. Kiểm tra mật khẩu
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new InvalidCredentialsException("Mật khẩu không chính xác");

            // 4. Tạo token
            string refreshToken = _authTokenUtil.GenerateRefreshToken(user.Id);
            string accessToken = _authTokenUtil.GenerateAccessToken(user.Id, user.Role);

            return new TokenPair(accessToken, refreshToken);
        }

        public string? Refresh(string refreshToken)
        {
            if (_authTokenUtil.IsTokenExpired(refreshToken)) return null;

            Guid userId = _authTokenUtil.ParseToken(refreshToken);
            var user = _repo.GetById(userId);
            if (user == null) return null;

            return _authTokenUtil.GenerateAccessToken(userId, user.Role);
        }

        public bool VerifyEmail(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            // 1. Tìm user theo token
            var user = _repo.GetByToken(token);
            if (user == null) return false;

            // 2. Đặt user đã xác thực
            user.Verified = true;
            user.Token = null;

            // 3. Cập nhật vào CSDL
            var updated = _repo.Update(user);
            return updated != null;
        }

        public User? GetUser(Guid id)
        {
            if (id == Guid.Empty) throw new InvalidUserDataException("ID không hợp lệ");
            return _repo.GetById(id);
        }

        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }
    }
}
