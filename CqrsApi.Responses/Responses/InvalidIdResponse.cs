namespace CqrsApi.Responses.Responses
{
    public class InvalidIdResponse
    {
        public string Message { get; set; } = "Id has invalid format. Id should be non-negative integer";
        public int StatusCode { get; set; } = 400;
    }
}