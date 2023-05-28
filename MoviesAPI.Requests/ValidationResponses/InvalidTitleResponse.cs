namespace MoviesAPI.Requests.ValidationResponses
{
    public class InvalidTitleResponse
    {
        public string Message { get; set; } = "Movie Title has invalid format. Price should be non-negative double.";
        public int StatusCode { get; set; } = 400;
    }
}