using System.ComponentModel.DataAnnotations.Schema;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("showtimes")]
    public class Showtime
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("movie_id")]
        public Guid MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        [Column("auditorium_id")]
        public Guid AuditoriumId { get; set; }

        [ForeignKey("AuditoriumId")]
        public Auditorium Auditorium { get; set; }

        [Column("start_time")]
        public DateTime StartTime { get; set; }

        [Column("end_time")]
        public DateTime EndTime { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
