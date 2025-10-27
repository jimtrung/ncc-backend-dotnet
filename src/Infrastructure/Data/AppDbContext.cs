using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Domain.Entities;

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
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<User>()
                .Property(u => u.Provider)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();
        }
    }
}
