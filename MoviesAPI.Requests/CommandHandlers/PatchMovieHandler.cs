using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MoviesAPI.Data.Context;
using MoviesAPI.Requests.CommandResponses;
using MoviesAPI.Requests.Commands;

namespace MoviesAPI.Requests.CommandHandlers;

public class PatchMovieHandler : IRequestHandler<PatchMovieCommand, PatchMovieSuccessResponse>
{
    private readonly MoviesContext _context;

    public PatchMovieHandler(MoviesContext context)
    {
        _context = context;
    }

    public async Task<PatchMovieSuccessResponse> Handle(PatchMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = _context.Movies.FirstOrDefault(x => x.MovieId == request.MovieId);
        if (movie == null)
            return null;

        movie.Title = request.Title;
        movie.Price = request.Price;
        movie.Year = request.Year;
        movie.AgeRestriction = request.AgeRestriction;
        await _context.SaveChangesAsync(cancellationToken);
        return new PatchMovieSuccessResponse(request.MovieId);
    }
}