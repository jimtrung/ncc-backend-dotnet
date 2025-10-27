using Microsoft.EntityFrameworkCore;
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

        public async Task<Auditorium> AddAsync(Auditorium auditorium)
        {
            _context.Auditoriums.Add(auditorium);
            await _context.SaveChangesAsync();
            return auditorium;
        }

        public async Task<List<Auditorium>> GetAllAsync()
        {
            return await _context.Auditoriums.ToListAsync();
        }

        public async Task<Auditorium?> GetByIdAsync(Guid id)
        {
            return await _context.Auditoriums.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> UpdateFieldAsync(Guid id, string fieldName, object value)
        {
            var auditorium = await _context.Auditoriums.FindAsync(id);
            if (auditorium == null)
                return false;

            var prop = typeof(Auditorium).GetProperty(fieldName, 
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Auditorium entity.");

            prop.SetValue(auditorium, value);
            _context.Auditoriums.Update(auditorium);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateByIdAsync(Guid id, Auditorium auditorium)
        {
            var existing = await _context.Auditoriums.FindAsync(id);
            if (existing == null)
                return false;

            existing.Capacity = auditorium.Capacity;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Auditoriums.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var auditorium = await _context.Auditoriums.FindAsync(id);
            if (auditorium == null)
                return false;

            _context.Auditoriums.Remove(auditorium);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllAsync()
        {
            var all = await _context.Auditoriums.ToListAsync();
            if (!all.Any())
                return false;

            _context.Auditoriums.RemoveRange(all);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
