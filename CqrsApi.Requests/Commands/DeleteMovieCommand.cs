using CqrsApi.Requests.CommandResponses;
using MediatR;

namespace CqrsApi.Requests.Commands
{
    public class DeleteMovieCommand : IRequest<DeleteMovieSuccessResponse>
    {
        public int MovieId { get; }

        public DeleteMovieCommand(int movieId)
        {
            MovieId = movieId;
        }
    }
}