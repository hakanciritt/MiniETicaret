namespace ETicaret.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException()
        {

        }

        public NotFoundUserException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
