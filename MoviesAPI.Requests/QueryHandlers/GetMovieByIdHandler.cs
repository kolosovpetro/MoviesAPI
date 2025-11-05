using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data.Context;
using MoviesAPI.Models.Models;
using MoviesAPI.Requests.Queries;

namespace MoviesAPI.Requests.QueryHandlers;

public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdQuery, Movie>
{
    private readonly MoviesContext _context;

    public GetMovieByIdHandler(MoviesContext context)
    {
        _context = context;
    }

    public async Task<Movie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Movies
            .FirstOrDefaultAsync(x => x.MovieId == request.Id, cancellationToken);
    }
}