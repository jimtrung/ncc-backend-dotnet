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
            // Step 1: Basic validation
            if (request.Username == null) throw new InvalidUserDataException("Username is empty");
            if (request.Email == null) throw new InvalidUserDataException("Email is empty");
            if (request.PhoneNumber == null) throw new InvalidUserDataException("Phone Number is empty");
            if (request.Password == null) throw new InvalidUserDataException("Password is empty");
            if (!_emailValidator.IsValidEmail(request.Email))
                throw new InvalidUserDataException("Invalid email");
            // Step 2: Check existing user
            var existingUser = _repo.GetByUsernameOrEmailOrPhoneNumber(request.Username, request.Email, request.PhoneNumber);
            if (existingUser != null) throw new UserAlreadyExistsException("username/email/phone number already exists");

            // Step 3: Generate token, otp, hash password
            string token = TokenUtil.GenerateToken();
            string otp = OtpUtil.GenerateOtp();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Step 4: Prepare user object
            User user = new()
            {
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = passwordHash,
                Token = token,
                OTP = otp,
                Role = UserRole.user,
                Provider = Provider.local,
                Verified = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Step 5: Send verification email (fire-and-forget)
            try
            {
                _emailValidator.SendVerificationEmail(user.Email, "http://localhost:8080/verify/" + token);
                Console.WriteLine("[DEBUG] Email send task started");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DEBUG] Email send failed: " + ex.Message);
            }

            // Step 6: Save to DB
            var savedUser = _repo.Add(user);

            return savedUser;
        }

        public TokenPair SignIn(SignInRequest request)
        {
            // Step 1: Basic validation
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new InvalidUserDataException("Username is empty");
            if (string.IsNullOrWhiteSpace(request.Password))
                throw new InvalidUserDataException("Password is empty");

            // Step 2: Check existing user
            var user = _repo.GetByUsername(request.Username)
                       ?? throw new UserNotFoundException("User does not exist");

            // Step 3: Check password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new InvalidCredentialsException("Wrong password");

            // Step 4: Generate tokens
            string refreshToken = _authTokenUtil.GenerateRefreshToken(user.Id);
            string accessToken = _authTokenUtil.GenerateAccessToken(user.Id);

            return new TokenPair(accessToken, refreshToken);
        }

        public string? Refresh(string refreshToken)
        {
            if (_authTokenUtil.IsTokenExpired(refreshToken)) return null;

            Guid userId = _authTokenUtil.ParseToken(refreshToken);
            return _authTokenUtil.GenerateAccessToken(userId);
        }

        public User? GetUser(Guid id)
        {
            if (id == Guid.Empty) throw new InvalidUserDataException("ID is empty");
            return _repo.GetById(id);
        }
    }
}
