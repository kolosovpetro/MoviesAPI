namespace CqrsApi.Responses.Responses
{
    public class InvalidYearResponse
    {
        public string Message { get; set; } = "Year has invalid format. Id should be non-negative integer";
        public int StatusCode { get; set; } = 500;
    }
}