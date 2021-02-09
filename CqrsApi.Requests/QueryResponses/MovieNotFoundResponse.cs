namespace CqrsApi.Requests.QueryResponses
{
    public class MovieNotFoundResponse
    {
        public string Message { get; }
        public int StatusCode { get; }

        public MovieNotFoundResponse(int movieId)
        {
            Message = $"Movie with id {movieId} not found.";
            StatusCode = 404;
        }
    }
}