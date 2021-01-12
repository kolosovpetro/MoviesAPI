namespace CqrsApi.Requests.CommandResponses
{
    public class PatchMovieSuccessResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public PatchMovieSuccessResponse(int id)
        {
            Message = $"Movie with id {id} has been updated.";
            StatusCode = 200;
        }
    }
}