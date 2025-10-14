namespace Theater_Management_BE.src.Domain.Exceptions.User
{
    public class InvalidUserDataException : Exception
    {
        public InvalidUserDataException(string message) : base(message) { }
    }
}
