namespace CqrsApi.Responses.Responses
{
    public class MovieNotFoundResponse
    {
        public int MovieId { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public MovieNotFoundResponse()
        {
        }

        public MovieNotFoundResponse(int movieId)
        {
            MovieId = movieId;
            Message = $"Movie with id {MovieId} not found.";
            StatusCode = 404;
        }
    }
}