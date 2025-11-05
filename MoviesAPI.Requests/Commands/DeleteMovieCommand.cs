using MediatR;
using MoviesAPI.Requests.CommandResponses;

namespace MoviesAPI.Requests.Commands;

public class DeleteMovieCommand : IRequest<DeleteMovieSuccessResponse>
{
    public int MovieId { get; }

    public DeleteMovieCommand(int movieId)
    {
        MovieId = movieId;
    }
}