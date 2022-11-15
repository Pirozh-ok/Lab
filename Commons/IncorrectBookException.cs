namespace BookAPI.Commons
{
    public class IncorrectBookException : Exception
    {
        public IncorrectBookException(string message) : base(message)
        { }
    }
}
