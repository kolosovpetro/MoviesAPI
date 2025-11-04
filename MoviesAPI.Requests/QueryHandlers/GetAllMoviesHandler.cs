using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data.Context;
using MoviesAPI.Models.Models;
using MoviesAPI.Requests.Queries;

namespace MoviesAPI.Requests.QueryHandlers
{
    public class GetAllMoviesHandler : IRequestHandler<GetAllMoviesQuery, IList<Movie>>
    {
        private readonly MoviesContext _context;

        public GetAllMoviesHandler(MoviesContext context)
        {
            _context = context;
        }

        public async Task<IList<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Movies.ToListAsync(cancellationToken);
        }
    }
}