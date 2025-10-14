namespace Theater_Management_BE.src.Domain.Exceptions.System
{
    public class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(string message) : base(message) { }
    }
}
