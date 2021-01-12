using CqrsApi.Requests.CommandResponses;
using MediatR;

namespace CqrsApi.Requests.Command
{
    public class PatchMovieCommand: IRequest<PatchMovieSuccessResponse>
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }
    }
}