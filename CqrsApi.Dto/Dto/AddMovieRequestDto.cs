namespace CqrsApi.Dto.Dto
{
    public class AddMovieRequestDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }
    }
}