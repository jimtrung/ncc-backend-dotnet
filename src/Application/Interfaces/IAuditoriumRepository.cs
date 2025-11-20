using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IAuditoriumRepository
    {
        Auditorium Add(Auditorium auditorium);
        List<Auditorium> GetAll();
        Auditorium? GetById(Guid id);
        bool UpdateField(Guid id, string fieldName, object value);
        bool UpdateById(Guid id, Auditorium auditorium);
        bool Delete(Guid id);
        bool DeleteAll();
    }
}
