using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Seat> AddAsync(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
            return seat;
        }

        public async Task<Seat?> GetByIdAsync(Guid id)
        {
            return await _context.Seats
                .Include(s => s.Auditorium)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Seat?> GetByFieldAsync(string fieldName, object fieldValue)
        {
            var property = typeof(Seat).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Invalid field name: {fieldName}");

            return await _context.Seats.FirstOrDefaultAsync(s =>
                EF.Property<object>(s, property.Name).Equals(fieldValue)
            );
        }

        public async Task<Seat?> UpdateFieldAsync(Guid id, string fieldName, object fieldValue)
        {
            var entity = await _context.Seats.FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                return null;

            var property = typeof(Seat).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Invalid field name: {fieldName}");

            property.SetValue(entity, fieldValue);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
                return false;

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
