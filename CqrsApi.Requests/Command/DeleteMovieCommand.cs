using CqrsApi.Requests.CommandResponses;
using MediatR;

namespace CqrsApi.Requests.Command
{
    public class DeleteMovieCommand : IRequest<DeleteMovieSuccessResponse>
    {
        public int MovieId { get; set; }

        public DeleteMovieCommand(int movieId)
        {
            MovieId = movieId;
        }
    }
}