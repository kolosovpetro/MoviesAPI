using CqrsApi.Responses.Responses;
using MediatR;

namespace CqrsApi.Commands.Commands
{
    // POST
    public class PostMovieCommand : IRequest<PostMovieSuccessResponse>
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }
    }
}