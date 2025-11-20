using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public Movie Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie? GetById(Guid id)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie? Update(Movie movie)
        {
            var existing = _context.Movies.Find(movie.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(movie);
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(Guid id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAll()
        {
            var allMovies = _context.Movies.ToList();
            if (!allMovies.Any()) return false;

            _context.Movies.RemoveRange(allMovies);
            _context.SaveChanges();
            return true;
        }
    }
}
