using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _context;

        public ActorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Actor> AddAsync(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            return actor;
        }

        public async Task<Actor?> GetByIdAsync(Guid id)
        {
            return await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Actor?> GetByFieldAsync(string fieldName, object value)
        {
            return fieldName.ToLower() switch
            {
                "id" => await _context.Actors.FirstOrDefaultAsync(a => a.Id == (Guid)value),
                "firstname" => await _context.Actors.FirstOrDefaultAsync(a => a.FirstName == (string)value),
                "lastname" => await _context.Actors.FirstOrDefaultAsync(a => a.LastName == (string)value),
                "countrycode" => await _context.Actors.FirstOrDefaultAsync(a => a.CountryCode == (string)value),
                _ => null
            };
        }

        public async Task<bool> UpdateFieldAsync(Guid id, string fieldName, object value)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return false;

            var prop = typeof(Actor).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Actor entity.");

            prop.SetValue(actor, value);
            _context.Actors.Update(actor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return false;

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
