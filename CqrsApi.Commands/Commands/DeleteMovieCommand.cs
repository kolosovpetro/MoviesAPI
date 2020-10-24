using CqrsApi.Responses.Responses;
using MediatR;

namespace CqrsApi.Commands.Commands
{
    public class DeleteMovieCommand : IRequest<DeleteSuccessResponse>
    {
        public int MovieId { get; set; }

        public DeleteMovieCommand(int movieId)
        {
            MovieId = movieId;
        }
    }
}