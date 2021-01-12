namespace CqrsApi.Responses.Responses
{
    public class InvalidPriceResponse
    {
        public string Message { get; set; } = "Movie Price has invalid format. Price should be non-negative double.";
        public int StatusCode { get; set; } = 400;
    }
}