using System.Reflection;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly AppDbContext _context;

        public DirectorRepository(AppDbContext context)
        {
            _context = context;
        }

        public Director Add(Director director)
        {
            _context.Directors.Add(director);
            _context.SaveChanges();
            return director;
        }

        public Director? GetById(Guid id)
        {
            return _context.Directors.FirstOrDefault(d => d.Id == id);
        }

        public Director? GetByField(string fieldName, object value)
        {
            return fieldName.ToLower() switch
            {
                "id" => _context.Directors.FirstOrDefault(d => d.Id == (Guid)value),
                "firstname" => _context.Directors.FirstOrDefault(d => d.FirstName == (string)value),
                "lastname" => _context.Directors.FirstOrDefault(d => d.LastName == (string)value),
                "countrycode" => _context.Directors.FirstOrDefault(d => d.CountryCode == (string)value),
                _ => null
            };
        }

        public bool UpdateByField(Guid id, string fieldName, object value)
        {
            var director = _context.Directors.Find(id);
            if (director == null) return false;

            var prop = typeof(Director).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Director entity.");

            prop.SetValue(director, value);
            _context.Directors.Update(director);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var director = _context.Directors.Find(id);
            if (director == null) return false;

            _context.Directors.Remove(director);
            _context.SaveChanges();
            return true;
        }

        public List<Director> GetAll()
        {
            return _context.Directors.ToList();
        }
    }
}
