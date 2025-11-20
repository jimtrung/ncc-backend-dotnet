using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Domain.Repositories
{
    public interface ITicketRepository
    {
        void Insert(Ticket ticket);
        Ticket? GetByField(string fieldName, object fieldValue);
        void UpdateByField(Guid id, string fieldName, object fieldValue);
        void Delete(Guid id);
    }
}
