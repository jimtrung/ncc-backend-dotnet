using Microsoft.EntityFrameworkCore;
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

        public async Task<Director> AddAsync(Director director)
        {
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return director;
        }

        public async Task<Director?> GetByIdAsync(Guid id)
        {
            return await _context.Directors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Director?> GetByFieldAsync(string fieldName, object value)
        {
            return fieldName.ToLower() switch
            {
                "id" => await _context.Directors.FirstOrDefaultAsync(d => d.Id == (Guid)value),
                "firstname" => await _context.Directors.FirstOrDefaultAsync(d => d.FirstName == (string)value),
                "lastname" => await _context.Directors.FirstOrDefaultAsync(d => d.LastName == (string)value),
                "countrycode" => await _context.Directors.FirstOrDefaultAsync(d => d.CountryCode == (string)value),
                _ => null
            };
        }

        public async Task<bool> UpdateByFieldAsync(Guid id, string fieldName, object value)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
                return false;

            var prop = typeof(Director).GetProperty(fieldName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Director entity.");

            prop.SetValue(director, value);
            _context.Directors.Update(director);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
                return false;

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
