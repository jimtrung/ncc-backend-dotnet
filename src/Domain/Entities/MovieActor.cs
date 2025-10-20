using System.ComponentModel.DataAnnotations.Schema;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("movie_actors")]
    public class MovieActor
    {
        [Column("movie_id")]
        public Guid MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        [Column("actor_id")]
        public Guid ActorId { get; set; }

        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
    }
}
