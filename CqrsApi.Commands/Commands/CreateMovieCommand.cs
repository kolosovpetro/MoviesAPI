using CqrsApi.Responses.Responses;
using MediatR;

namespace CqrsApi.Commands.Commands
{
    // POST
    public class CreateMovieCommand : IRequest<MovieAddSuccessResponse>
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }
    }
}