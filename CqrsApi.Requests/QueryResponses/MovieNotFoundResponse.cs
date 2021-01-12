namespace CqrsApi.Requests.QueryResponses
{
    public class MovieNotFoundResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public MovieNotFoundResponse(int movieId)
        {
            Message = $"Movie with id {movieId} not found.";
            StatusCode = 404;
        }
    }
}