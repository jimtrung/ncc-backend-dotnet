using Microsoft.EntityFrameworkCore;
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

        public async Task<Movie> AddAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> UpdateAsync(Movie movie)
        {
            var existing = await _context.Movies.FindAsync(movie.Id);
            if (existing == null)
                return null;

            _context.Entry(existing).CurrentValues.SetValues(movie);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllAsync()
        {
            var allMovies = await _context.Movies.ToListAsync();
            if (!allMovies.Any())
                return false;

            _context.Movies.RemoveRange(allMovies);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
