using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IActorRepository
    {
        Task<Actor> AddAsync(Actor actor);
        Task<Actor?> GetByIdAsync(Guid id);
        Task<Actor?> GetByFieldAsync(string fieldName, object value);
        Task<bool> UpdateFieldAsync(Guid id, string fieldName, object value);
        Task<bool> DeleteAsync(Guid id);
    }
}
