using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Domain.Repositories
{
    public interface ITicketRepository
    {
        Task InsertAsync(Ticket ticket);
        Task<Ticket?> GetByFieldAsync(string fieldName, object fieldValue);
        Task UpdateByFieldAsync(Guid id, string fieldName, object fieldValue);
        Task DeleteAsync(Guid id);
    }
}
