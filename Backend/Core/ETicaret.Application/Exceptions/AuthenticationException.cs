namespace ETicaret.Application.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() { }
        public AuthenticationException(string errorMessage) : base(errorMessage) { }
    }
}
