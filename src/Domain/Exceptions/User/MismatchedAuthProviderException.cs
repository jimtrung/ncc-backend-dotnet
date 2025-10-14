namespace Theater_Management_BE.src.Domain.Exceptions.User
{
    public class MismatchedAuthProviderException : Exception
    {
        public MismatchedAuthProviderException(string message) : base(message) { }
    }
}
