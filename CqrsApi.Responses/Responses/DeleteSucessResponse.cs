namespace CqrsApi.Responses.Responses
{
    public class DeleteSuccessResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public DeleteSuccessResponse(int id)
        {
            Message = $"Movie with id {id} has been deleted.";
            StatusCode = 200;
        }
    }
}