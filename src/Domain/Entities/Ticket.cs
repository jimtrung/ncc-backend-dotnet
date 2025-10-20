using System.ComponentModel.DataAnnotations.Schema;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("tickets")]
    public class Ticket
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column("showtime_id")]
        public Guid ShowtimeId { get; set; }

        [ForeignKey("ShowtimeId")]
        public Showtime Showtime { get; set; }

        [Column("seat_id")]
        public Guid SeatId { get; set; }

        [ForeignKey("SeatId")]
        public Seat Seat { get; set; }

        [Column("price")]
        public int Price { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
