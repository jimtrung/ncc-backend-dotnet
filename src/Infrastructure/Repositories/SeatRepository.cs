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

        public Seat Add(Seat seat)
        {
            _context.Seats.Add(seat);
            _context.SaveChanges();
            return seat;
        }

        public Seat? GetById(Guid id)
        {
            return _context.Seats
                .Include(s => s.Auditorium)
                .FirstOrDefault(s => s.Id == id);
        }

        public Seat? GetByField(string fieldName, object fieldValue)
        {
            var property = typeof(Seat).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Invalid field name: {fieldName}");

            return _context.Seats.FirstOrDefault(s =>
                EF.Property<object>(s, property.Name).Equals(fieldValue)
            );
        }

        public Seat? UpdateField(Guid id, string fieldName, object fieldValue)
        {
            var entity = _context.Seats.FirstOrDefault(s => s.Id == id);
            if (entity == null) return null;

            var property = typeof(Seat).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Invalid field name: {fieldName}");

            property.SetValue(entity, fieldValue);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(Guid id)
        {
            var seat = _context.Seats.Find(id);
            if (seat == null) return false;

            _context.Seats.Remove(seat);
            _context.SaveChanges();
            return true;
        }
    }
}
