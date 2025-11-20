using System.Runtime.Serialization;

namespace Theater_Management_BE.src.Domain.Entities
{
    public enum Provider
    {
        [EnumMember(Value = "LOCAL")]
        LOCAL,
        [EnumMember(Value = "GOOGLE")]
        GOOGLE
    }
}
