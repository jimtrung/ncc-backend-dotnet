using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? GetById(Guid value)
        {
            return _context.Users.FirstOrDefault(u => u.Id == value);
        }

        public User? GetByUsername(string value)
        {
            return _context.Users.FirstOrDefault(u => u.Username == value);
        }

        public User? GetByEmail(string value)
        {
            return _context.Users.FirstOrDefault(u => u.Email == value);
        }

        public User? GetByPhoneNumber(string value)
        {
            return _context.Users.FirstOrDefault(u => u.PhoneNumber == value);
        }

        public User? Update(User user)
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser == null)
                return null;

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            _context.SaveChanges();
            return existingUser;
        }

        public bool Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public User? GetByUsernameOrEmailOrPhoneNumber(string username, string email, string phoneNumber)
        {
            return _context.Users.FirstOrDefault(u =>
                (u.Username == username) || (u.Email == email) || (u.PhoneNumber == phoneNumber)
            );
        }
    }
}
