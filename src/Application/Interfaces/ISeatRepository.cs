using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface ISeatRepository
    {
        Seat Add(Seat seat);
        Seat? GetById(Guid id);
        Seat? GetByField(string fieldName, object fieldValue);
        Seat? UpdateField(Guid id, string fieldName, object fieldValue);
        bool Delete(Guid id);
    }
}
