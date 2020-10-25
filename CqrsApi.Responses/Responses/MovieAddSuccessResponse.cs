namespace CqrsApi.Responses.Responses
{
    public class MovieAddSuccessResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public MovieAddSuccessResponse(string title)
        {
            Message = $"Movie {title} has been added successfully.";
            StatusCode = 200;
        }
    }
}