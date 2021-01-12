namespace CqrsApi.Responses.Responses
{
    public class PostMovieSuccessResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public PostMovieSuccessResponse(string title)
        {
            Message = $"Movie {title} has been added successfully.";
            StatusCode = 200;
        }
    }
}