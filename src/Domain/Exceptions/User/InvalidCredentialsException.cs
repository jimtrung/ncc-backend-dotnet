namespace Theater_Management_BE.src.Domain.Exceptions.User
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message) : base(message) { }
    }
}
