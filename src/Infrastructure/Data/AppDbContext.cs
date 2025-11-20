using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Domain.Enums;

namespace Theater_Management_BE.src.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Auditorium> Auditoriums { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieActor> MovieActors { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Showtime> Showtimes { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Register PostgreSQL enum types
            modelBuilder.HasPostgresEnum<Provider>("provider_type");
            modelBuilder.HasPostgresEnum<UserRole>("role_type");
            modelBuilder.HasPostgresEnum<MovieGenre>("movie_genre");

            // Configure User enum properties
            // Npgsql will handle conversion via MapEnum in Program.cs
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasColumnType("role_type");

            modelBuilder.Entity<User>()
                .Property(u => u.Provider)
                .HasColumnType("provider_type");

            // Configure Movie enum properties (array of enums)
            modelBuilder.Entity<Movie>()
                .Property(m => m.Genres)
                .HasColumnType("movie_genre[]");

            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
