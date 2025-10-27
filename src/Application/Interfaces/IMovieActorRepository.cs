using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IMovieActorRepository
    {
        Task<MovieActor> AddAsync(MovieActor movieActor);
        Task<MovieActor?> GetByIdsAsync(Guid movieId, Guid actorId);
        Task<MovieActor?> GetByFieldAsync(string fieldName, object value);
        Task<MovieActor?> UpdateFieldAsync(Guid movieId, Guid actorId, string fieldName, object fieldValue);
        Task<bool> DeleteAsync(Guid movieId, Guid actorId);
    }
}
