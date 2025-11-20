using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IUserRepository
    {
        User Add(User user);
        User? GetById(Guid value);
        User? GetByUsername(string value);
        User? GetByEmail(string value);
        User? GetByPhoneNumber(string value);
        User? Update(User user);
        bool Delete(Guid userId);
        User? GetByUsernameOrEmailOrPhoneNumber(string username, string email, string phoneNumber);
    }
}
