using System.ComponentModel.DataAnnotations.Schema;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("seats")]
    public class Seat
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("auditorium_id")]
        public Guid AuditoriumId { get; set; }

        [ForeignKey("AuditoriumId")]
        public Auditorium Auditorium { get; set; }

        [Column("row")]
        public string Row { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
