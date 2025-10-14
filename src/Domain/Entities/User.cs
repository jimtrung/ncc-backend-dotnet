using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Theater_Management_BE.src.Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role")]
        public UserRole Role { get; set; }

        [Column("provider")]
        public Provider Provider { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("otp")]
        public int OTP { get; set; }

        [Column("verified")]
        public bool Verified { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
