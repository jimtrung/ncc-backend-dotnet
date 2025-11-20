using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class MovieActorRepository : IMovieActorRepository
    {
        private readonly AppDbContext _context;

        public MovieActorRepository(AppDbContext context)
        {
            _context = context;
        }

        public MovieActor Add(MovieActor movieActor)
        {
            _context.MovieActors.Add(movieActor);
            _context.SaveChanges();
            return movieActor;
        }

        public MovieActor? GetByIds(Guid movieId, Guid actorId)
        {
            return _context.MovieActors.FirstOrDefault(ma => ma.MovieId == movieId && ma.ActorId == actorId);
        }

        public MovieActor? GetByField(string fieldName, object value)
        {
            var property = typeof(MovieActor).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Invalid field name: {fieldName}");

            return _context.MovieActors.FirstOrDefault(ma =>
                EF.Property<object>(ma, property.Name).Equals(value)
            );
        }

        public MovieActor? UpdateField(Guid movieId, Guid actorId, string fieldName, object fieldValue)
        {
            var entity = GetByIds(movieId, actorId);
            if (entity == null) return null;

            var property = typeof(MovieActor).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Invalid field name: {fieldName}");

            property.SetValue(entity, fieldValue);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(Guid movieId, Guid actorId)
        {
            var entity = GetByIds(movieId, actorId);
            if (entity == null) return false;

            _context.MovieActors.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
