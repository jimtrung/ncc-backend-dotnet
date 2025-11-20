using System.ComponentModel.DataAnnotations.Schema;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("directors")]
    public class Director
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("dob")]
        public DateTime? Dob { get; set; }

        [Column("age")]
        public int? Age { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("country_code")]
        public string CountryCode { get; set; }

        [ForeignKey("CountryCode")]
        public Country Country { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
