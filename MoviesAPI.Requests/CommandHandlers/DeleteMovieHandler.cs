using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data.Context;
using MoviesAPI.Requests.CommandResponses;
using MoviesAPI.Requests.Commands;

namespace MoviesAPI.Requests.CommandHandlers;

public class DeleteMovieHandler : IRequestHandler<DeleteMovieCommand, DeleteMovieSuccessResponse>
{
    private readonly MoviesContext _context;

    public DeleteMovieHandler(MoviesContext context)
    {
        _context = context;
    }

    public async Task<DeleteMovieSuccessResponse> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.MovieId == request.MovieId,
            cancellationToken);
        if (movie == null)
            return null;
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteMovieSuccessResponse(request.MovieId);
    }
}