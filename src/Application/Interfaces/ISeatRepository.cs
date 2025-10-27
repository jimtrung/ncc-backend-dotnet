using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface ISeatRepository
    {
        Task<Seat> AddAsync(Seat seat);
        Task<Seat?> GetByIdAsync(Guid id);
        Task<Seat?> GetByFieldAsync(string fieldName, object fieldValue);
        Task<Seat?> UpdateFieldAsync(Guid id, string fieldName, object fieldValue);
        Task<bool> DeleteAsync(Guid id);
    }
}
