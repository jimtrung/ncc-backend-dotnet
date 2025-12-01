using System.ComponentModel.DataAnnotations.Schema;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("auditoriums")]
    public class Auditorium
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
