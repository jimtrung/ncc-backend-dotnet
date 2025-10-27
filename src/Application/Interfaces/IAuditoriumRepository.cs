using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IAuditoriumRepository
    {
        Task<Auditorium> AddAsync(Auditorium auditorium);
        Task<List<Auditorium>> GetAllAsync();
        Task<Auditorium?> GetByIdAsync(Guid id);
        Task<bool> UpdateFieldAsync(Guid id, string fieldName, object value);
        Task<bool> UpdateByIdAsync(Guid id, Auditorium auditorium);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAllAsync();
    }
}
