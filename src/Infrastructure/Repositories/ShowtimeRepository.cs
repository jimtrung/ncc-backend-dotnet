using System.Reflection;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
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

        public Showtime Add(Showtime showtime)
        {
            _context.Showtimes.Add(showtime);
            _context.SaveChanges();
            return showtime;
        }

        public List<Showtime> GetAll()
        {
            return _context.Showtimes.ToList();
        }

        public Showtime? GetById(Guid id)
        {
            return _context.Showtimes.FirstOrDefault(s => s.Id == id);
        }

        public Showtime? GetByField(string fieldName, object value)
        {
            var prop = typeof(Showtime).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Showtime entity.");

            return _context.Showtimes
                .AsEnumerable()
                .FirstOrDefault(s => prop.GetValue(s)?.Equals(value) ?? false);
        }

        public bool UpdateByField(Guid id, string fieldName, object value)
        {
            var showtime = _context.Showtimes.Find(id);
            if (showtime == null) return false;

            var prop = typeof(Showtime).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Showtime entity.");

            prop.SetValue(showtime, value);
            _context.Showtimes.Update(showtime);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var showtime = _context.Showtimes.Find(id);
            if (showtime == null) return false;

            _context.Showtimes.Remove(showtime);
            _context.SaveChanges();
            return true;
        }

        public void DeleteAll()
        {
            var all = _context.Showtimes.ToList();
            if (!all.Any()) return;

            _context.Showtimes.RemoveRange(all);
            _context.SaveChanges();
        }
    }
}
