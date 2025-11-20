using System.Runtime.Serialization;

namespace Theater_Management_BE.src.Domain.Entities
{
    public enum UserRole
    {
        [EnumMember(Value = "USER")]
        USER,
        [EnumMember(Value = "ADMINISTARTOR")]
        ADMINISTARTOR,
        [EnumMember(Value = "MODERATOR")]
        MODERATOR
    }
}
