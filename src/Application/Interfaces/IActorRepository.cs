using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IActorRepository
    {
        Actor Add(Actor actor);
        Actor? GetById(Guid id);
        Actor? GetByField(string fieldName, object value);
        List<Actor> GetAll();
        bool UpdateField(Guid id, string fieldName, object value);
        bool Delete(Guid id);
    }
}
