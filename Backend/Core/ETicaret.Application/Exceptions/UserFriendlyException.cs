namespace ETicaret.Application.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException()
        {
            
        }
        public UserFriendlyException(string errorMessage) : base(errorMessage)
        {
            
        }
    }
}
