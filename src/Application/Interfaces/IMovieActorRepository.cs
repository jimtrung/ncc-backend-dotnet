using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IMovieActorRepository
    {
        MovieActor Add(MovieActor movieActor);
        MovieActor? GetByIds(Guid movieId, Guid actorId);
        MovieActor? GetByField(string fieldName, object value);
        MovieActor? UpdateField(Guid movieId, Guid actorId, string fieldName, object fieldValue);
        bool Delete(Guid movieId, Guid actorId);
    }
}
