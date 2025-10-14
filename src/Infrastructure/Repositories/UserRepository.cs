using Microsoft.EntityFrameworkCore;
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

        public async Task<User> AddAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByIdAsync(Guid value)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == value);
        }

        public async Task<User?> GetByUsernameAsync(string value)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == value);
        }

        public async Task<User?> GetByEmailAsync(string value)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == value);
        }

        public async Task<User?> GetByPhoneNumberAsync(string value)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == value);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser = user;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
