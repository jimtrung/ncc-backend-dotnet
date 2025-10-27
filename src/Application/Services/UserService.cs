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

        public async Task<User> SignUp(SignUpRequest request)
        {
            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine("[DEBUG] SignUp started at " + DateTime.UtcNow);

            // Step 1: Basic validation
            var t1 = stopwatch.ElapsedMilliseconds;
            if (request.Username == null) throw new InvalidUserDataException("Username is empty");
            if (request.Email == null) throw new InvalidUserDataException("Email is empty");
            if (request.PhoneNumber == null) throw new InvalidUserDataException("Phone Number is empty");
            if (request.Password == null) throw new InvalidUserDataException("Password is empty");
            if (!await _emailValidator.IsValidEmailAsync(request.Email))
                throw new InvalidUserDataException("Invalid email");
            Console.WriteLine($"[DEBUG] Step 1 done in {stopwatch.ElapsedMilliseconds - t1} ms");

            // Step 2: Check existing user
            var t2 = stopwatch.ElapsedMilliseconds;
            var existingUser = await _repo.GetByUsernameOrEmailOrPhoneNumber(request.Username, request.Email, request.PhoneNumber);
            if (existingUser != null) throw new UserAlreadyExistsException("username/email/phone number already exists");
            Console.WriteLine($"[DEBUG] Step 2 done in {stopwatch.ElapsedMilliseconds - t2} ms");

            // Step 3: Generate token, otp, hash password
            var t3 = stopwatch.ElapsedMilliseconds;
            string token = TokenUtil.GenerateToken();
            int otp = OtpUtil.GenerateOtp();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            Console.WriteLine($"[DEBUG] Step 3 done in {stopwatch.ElapsedMilliseconds - t3} ms");

            // Step 4: Prepare user object
            var t4 = stopwatch.ElapsedMilliseconds;
            User user = new()
            {
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = passwordHash,
                Token = token,
                OTP = otp,
                Role = UserRole.USER,
                Provider = Provider.LOCAL,
                Verified = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            Console.WriteLine($"[DEBUG] Step 4 done in {stopwatch.ElapsedMilliseconds - t4} ms");

            // Step 5: Send verification email
            var emailTask = Task.Run(() =>
              {
                  try
                  {
                      _ = _emailValidator.SendVerificationEmailAsync(user.Email, "http://localhost:8080/verify/" + token);
                      Console.WriteLine("[DEBUG] Email send task started");
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine("[DEBUG] Email send failed: " + ex.Message);
                  }
              });

            // Step 6: Save to DB
            var t6 = stopwatch.ElapsedMilliseconds;
            var savedUser = await _repo.AddAsync(user);
            Console.WriteLine($"[DEBUG] Step 6 done in {stopwatch.ElapsedMilliseconds - t6} ms");

            stopwatch.Stop();
            Console.WriteLine($"[DEBUG] Total SignUp time: {stopwatch.ElapsedMilliseconds} ms");

            return savedUser;
        }

        public async Task<TokenPair> SignIn(SignInRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine("[DEBUG] SignIn started at " + DateTime.UtcNow);

            // Step 1: Basic validation
            var t1 = stopwatch.ElapsedMilliseconds;
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new InvalidUserDataException("Username is empty");
            if (string.IsNullOrWhiteSpace(request.Password))
                throw new InvalidUserDataException("Password is empty");
            Console.WriteLine($"[DEBUG] Step 1 done in {stopwatch.ElapsedMilliseconds - t1} ms");

            // Step 2: Check existing user
            var t2 = stopwatch.ElapsedMilliseconds;
            var user = await _repo.GetByUsernameAsync(request.Username)
                       ?? throw new UserNotFoundException("User does not exist");
            Console.WriteLine($"[DEBUG] Step 2 done in {stopwatch.ElapsedMilliseconds - t2} ms");

            // Step 3: Check user password (use BCrypt Verify, not ReferenceEquals)
            var t3 = stopwatch.ElapsedMilliseconds;
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new InvalidCredentialsException("Wrong password");
            Console.WriteLine($"[DEBUG] Step 3 done in {stopwatch.ElapsedMilliseconds - t3} ms");

            // Step 4: Generate tokens (can be done in parallel)
            var t4 = stopwatch.ElapsedMilliseconds;
            var refreshTask = Task.Run(() => _authTokenUtil.GenerateRefreshToken(user.Id));
            var accessTask = Task.Run(() => _authTokenUtil.GenerateAccessToken(user.Id));

            await Task.WhenAll(refreshTask, accessTask);

            var tokenPair = new TokenPair
            {
                RefreshToken = refreshTask.Result,
                AccessToken = accessTask.Result
            };
            Console.WriteLine($"[DEBUG] Step 4 done in {stopwatch.ElapsedMilliseconds - t4} ms");

            stopwatch.Stop();
            Console.WriteLine($"[DEBUG] Total SignIn time: {stopwatch.ElapsedMilliseconds} ms");

            return tokenPair;
        }

        public string? Refresh(String refreshToken)
        {
            if (_authTokenUtil.IsTokenExpired(refreshToken)) return null;

            Guid userId = _authTokenUtil.ParseToken(refreshToken);
            return _authTokenUtil.GenerateAccessToken(userId);
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            if (id == null) throw new InvalidUserDataException("ID is empty");
            return await _repo.GetByIdAsync(id);
        }
    }
}
