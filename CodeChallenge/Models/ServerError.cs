namespace CodeChallenge.Models
{
    public class ServerError
    {
        public string Message { get; }
        public ServerError(string message)
        {
            Message = message;
        }
    }
}

