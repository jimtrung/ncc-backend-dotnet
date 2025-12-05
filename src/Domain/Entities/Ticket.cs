using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("tickets")]
    public class Ticket
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("userid")]
        public Guid Userid { get; set; }

        [Column("showtimeid")]
        public Guid Showtimeid { get; set; }

        [Column("price")]
        public int Price { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("seatname")]
        public String Seatname { get; set; }
    }
}

