using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IMovieRepository
    {
        Movie Add(Movie movie);
        Movie? GetById(Guid id);
        IEnumerable<Movie> GetAll();
        Movie? Update(Movie movie);
        bool Delete(Guid id);
        bool DeleteAll();
    }
}
