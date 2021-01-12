namespace CqrsApi.Requests.CommandResponses
{
    public class DeleteMovieSuccessResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public DeleteMovieSuccessResponse(int id)
        {
            Message = $"Movie with id {id} has been deleted.";
            StatusCode = 200;
        }
    }
}