using CqrsApi.Responses.Responses;
using MediatR;

namespace CqrsApi.Commands.Commands
{
    public class UpdateMovieCommand: IRequest<UpdateSuccessResponse>
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }
    }
}