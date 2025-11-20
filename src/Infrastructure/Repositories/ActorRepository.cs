using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
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

        public Actor Add(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();
            return actor;
        }

        public Actor? GetById(Guid id)
        {
            return _context.Actors.FirstOrDefault(a => a.Id == id);
        }

        public Actor? GetByField(string fieldName, object value)
        {
            return fieldName.ToLower() switch
            {
                "id" => _context.Actors.FirstOrDefault(a => a.Id == (Guid)value),
                "firstname" => _context.Actors.FirstOrDefault(a => a.FirstName == (string)value),
                "lastname" => _context.Actors.FirstOrDefault(a => a.LastName == (string)value),
                "countrycode" => _context.Actors.FirstOrDefault(a => a.CountryCode == (string)value),
                _ => null
            };
        }

        public List<Actor> GetAll()
        {
            return _context.Actors.ToList();
        }

        public bool UpdateField(Guid id, string fieldName, object value)
        {
            var actor = _context.Actors.Find(id);
            if (actor == null) return false;

            var prop = typeof(Actor).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Actor entity.");

            prop.SetValue(actor, value);
            _context.Actors.Update(actor);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var actor = _context.Actors.Find(id);
            if (actor == null) return false;

            _context.Actors.Remove(actor);
            _context.SaveChanges();
            return true;
        }
    }
}
