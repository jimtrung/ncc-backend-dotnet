using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Domain.Repositories;
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

        public void Insert(Showtime showtime)
        {
            try
            {
                _context.Showtimes.Add(showtime);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert new showtime", ex);
            }
        }

        public Showtime? GetByField(string fieldName, object fieldValue)
        {
            try
            {
                var property = typeof(Showtime).GetProperty(fieldName);
                if (property == null)
                    throw new Exception($"Invalid field name: {fieldName}");

                return _context.Showtimes
                    .AsEnumerable()
                    .FirstOrDefault(s => property.GetValue(s)?.Equals(fieldValue) ?? false);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get showtime", ex);
            }
        }

        public void UpdateByField(Guid id, string fieldName, object fieldValue)
        {
            try
            {
                var showtime = _context.Showtimes.Find(id);
                if (showtime == null)
                    throw new Exception($"Showtime with ID {id} not found");

                var property = typeof(Showtime).GetProperty(fieldName);
                if (property == null)
                    throw new Exception($"Invalid field name: {fieldName}");

                property.SetValue(showtime, fieldValue);
                _context.Showtimes.Update(showtime);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update showtime", ex);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var showtime = _context.Showtimes.Find(id);
                if (showtime == null)
                    throw new Exception($"Showtime with ID {id} not found");

                _context.Showtimes.Remove(showtime);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete showtime", ex);
            }
        }
    }
}
