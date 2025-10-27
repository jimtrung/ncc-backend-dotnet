using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> AddAsync(Movie movie);
        Task<Movie?> GetByIdAsync(Guid id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> UpdateAsync(Movie movie);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAllAsync();
    }
}
