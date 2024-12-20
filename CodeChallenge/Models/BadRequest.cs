namespace CodeChallenge.Models
{
    public class BadRequest
    {
        public string Message { get; }
        public BadRequest(string message)
        {
            Message = message;
        }
    }
}