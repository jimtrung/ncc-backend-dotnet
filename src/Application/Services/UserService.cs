using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Domain.Exceptions.User;
using Theater_Management_BE.src.Infrastructure.Utils;

namespace Theater_Management_BE.src.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> SignUp(User user)
        {
            if (user.Username == null) throw new InvalidUserDataException("Username is empty");
            if (user.Email == null) throw new InvalidUserDataException("Email is empty");
            if (user.PhoneNumber == null) throw new InvalidUserDataException("Phone Number is empty");
            if (user.Password == null) throw new InvalidUserDataException("Password is empty");

            var existingUser = await _repo.GetByUsernameAsync(user.Username);
            existingUser = await _repo.GetByEmailAsync(user.Email);
            existingUser = await _repo.GetByPhoneNumberAsync(user.PhoneNumber);
            if (existingUser != null) throw new UserAlreadyExistsException("username/email/phone number already exists");

            string token = TokenUtil.GenerateToken();
            int otp = OtpUtil.GenerateOtp();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Token = token;
            user.OTP = otp;
            user.Password = passwordHash;
            user.Role = UserRole.USER;
            user.Provider = Provider.LOCAL;
            user.Verified = false;

            return await _repo.AddAsync(user);
        }
    }
}
