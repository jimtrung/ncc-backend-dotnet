using System.Reflection;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class AuditoriumRepository : IAuditoriumRepository
    {
        private readonly AppDbContext _context;

        public AuditoriumRepository(AppDbContext context)
        {
            _context = context;
        }

        public Auditorium Add(Auditorium auditorium)
        {
            _context.Auditoriums.Add(auditorium);
            _context.SaveChanges();
            return auditorium;
        }

        public List<Auditorium> GetAll()
        {
            return _context.Auditoriums.ToList();
        }

        public Auditorium? GetById(Guid id)
        {
            return _context.Auditoriums.FirstOrDefault(a => a.Id == id);
        }

        public bool UpdateField(Guid id, string fieldName, object value)
        {
            var auditorium = _context.Auditoriums.Find(id);
            if (auditorium == null) return false;

            var prop = typeof(Auditorium).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Auditorium entity.");

            prop.SetValue(auditorium, value);
            _context.Auditoriums.Update(auditorium);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateById(Guid id, Auditorium auditorium)
        {
            var existing = _context.Auditoriums.Find(id);
            if (existing == null) return false;

            // Chỉ update nếu client gửi giá trị
            if (!string.IsNullOrWhiteSpace(auditorium.Name))
                existing.Name = auditorium.Name;

            if (!string.IsNullOrWhiteSpace(auditorium.Type))
                existing.Type = auditorium.Type;

            if (!string.IsNullOrWhiteSpace(auditorium.Note))
                existing.Note = auditorium.Note;

            if (auditorium.Capacity > 0)
                existing.Capacity = auditorium.Capacity;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Auditoriums.Update(existing);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var auditorium = _context.Auditoriums.Find(id);
            if (auditorium == null) return false;

            _context.Auditoriums.Remove(auditorium);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAll()
        {
            var all = _context.Auditoriums.ToList();
            if (!all.Any()) return false;

            _context.Auditoriums.RemoveRange(all);
            _context.SaveChanges();
            return true;
        }
    }
}
