using System.ComponentModel.DataAnnotations.Schema;
using Theater_Management_BE.src.Domain.Enums;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("movies")]
    public class Movie
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("director_id")]
        public Guid? DirectorId { get; set; }

        [Column("genres")]
        public List<MovieGenre> Genres { get; set; }

        [Column("premiere")]
        public DateTime? Premiere { get; set; }

        [Column("duration")]
        public int? Duration { get; set; }

        [Column("language")]
        public string Language { get; set; }

        [Column("rated")]
        public int? Rated { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
