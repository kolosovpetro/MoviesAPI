namespace CqrsApi.Requests.ValidationResponses
{
    public class InvalidYearResponse
    {
        public string Message { get; set; } = "Movie Year has invalid format. Year should be non-negative integer grater than 1887.";
        public int StatusCode { get; set; } = 400;
    }
}