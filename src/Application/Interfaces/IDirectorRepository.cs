using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IDirectorRepository
    {
        Task<Director> AddAsync(Director director);
        Task<Director?> GetByIdAsync(Guid id);
        Task<Director?> GetByFieldAsync(string fieldName, object value);
        Task<bool> UpdateByFieldAsync(Guid id, string fieldName, object value);
        Task<bool> DeleteAsync(Guid id);
    }
}
