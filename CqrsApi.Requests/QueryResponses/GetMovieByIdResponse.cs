namespace CqrsApi.Requests.QueryResponses
{
    public class GetMovieByIdResponse
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }
    }
}