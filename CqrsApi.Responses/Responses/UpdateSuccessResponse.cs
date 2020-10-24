namespace CqrsApi.Responses.Responses
{
    public class UpdateSuccessResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public UpdateSuccessResponse(int id)
        {
            Message = $"Movie with id {id} has been updated.";
            StatusCode = 200;
        }
    }
}