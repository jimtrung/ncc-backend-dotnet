namespace Theater_Management_BE.src.Domain.Exceptions.User
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
