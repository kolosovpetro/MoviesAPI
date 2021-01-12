namespace CqrsApi.Responses.Responses
{
    public class InvalidAgeRestrictionResponse
    {
        public string Message { get; set; } = "Movie Age restriction has invalid format. Age restriction should be non-negative integer";
        public int StatusCode { get; set; } = 400;
    }
}