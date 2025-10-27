using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IShowtimeRepository
    {
        Task InsertAsync(Showtime showtime);
        Task<Showtime?> GetByFieldAsync(string fieldName, object fieldValue);
        Task UpdateByFieldAsync(Guid id, string fieldName, object fieldValue);
        Task DeleteAsync(Guid id);
    }
}
