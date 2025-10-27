using Microsoft.EntityFrameworkCore;
using System.Reflection;
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

        public async Task<MovieActor> AddAsync(MovieActor movieActor)
        {
            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();
            return movieActor;
        }

        public async Task<MovieActor?> GetByIdsAsync(Guid movieId, Guid actorId)
        {
            return await _context.MovieActors
                .FirstOrDefaultAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId);
        }

        public async Task<MovieActor?> GetByFieldAsync(string fieldName, object value)
        {
            var property = typeof(MovieActor).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Invalid field name: {fieldName}");

            return await _context.MovieActors.FirstOrDefaultAsync(ma =>
                EF.Property<object>(ma, property.Name).Equals(value)
            );
        }

        public async Task<MovieActor?> UpdateFieldAsync(Guid movieId, Guid actorId, string fieldName, object fieldValue)
        {
            var entity = await GetByIdsAsync(movieId, actorId);
            if (entity == null)
                return null;

            var property = typeof(MovieActor).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Invalid field name: {fieldName}");

            property.SetValue(entity, fieldValue);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid movieId, Guid actorId)
        {
            var entity = await GetByIdsAsync(movieId, actorId);
            if (entity == null)
                return false;

            _context.MovieActors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
