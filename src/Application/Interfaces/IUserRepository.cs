using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User?> GetByIdAsync(Guid value);
        Task<User?> GetByUsernameAsync(string value);
        Task<User?> GetByEmailAsync(string value);
        Task<User?> GetByPhoneNumberAsync(string value);
        Task<User?> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid userId);
        Task<User?> GetByUsernameOrEmailOrPhoneNumber(string username, string email, string phoneNumber);
    }
}
