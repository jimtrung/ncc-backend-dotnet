using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly AppDbContext _context;

        public ShowtimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Showtime showtime)
        {
            try
            {
                await _context.Showtimes.AddAsync(showtime);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert new showtime", ex);
            }
        }

        public async Task<Showtime?> GetByFieldAsync(string fieldName, object fieldValue)
        {
            try
            {
                // 🧠 Dùng reflection cho linh hoạt như Java
                var query = _context.Showtimes.AsQueryable();
                var parameter = typeof(Showtime).GetProperty(fieldName);
                if (parameter == null)
                    throw new Exception($"Invalid field name: {fieldName}");

                return await query.FirstOrDefaultAsync(s => EF.Property<object>(s, fieldName).Equals(fieldValue));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get showtime", ex);
            }
        }

        public async Task UpdateByFieldAsync(Guid id, string fieldName, object fieldValue)
        {
            try
            {
                var showtime = await _context.Showtimes.FindAsync(id);
                if (showtime == null)
                    throw new Exception($"Showtime with ID {id} not found");

                var property = typeof(Showtime).GetProperty(fieldName);
                if (property == null)
                    throw new Exception($"Invalid field name: {fieldName}");

                property.SetValue(showtime, fieldValue);
                _context.Showtimes.Update(showtime);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update showtime", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var showtime = await _context.Showtimes.FindAsync(id);
                if (showtime == null)
                    throw new Exception($"Showtime with ID {id} not found");

                _context.Showtimes.Remove(showtime);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete showtime", ex);
            }
        }
    }
}
