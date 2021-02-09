using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Data.Context;
using CqrsApi.Models.Models;
using CqrsApi.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsApi.Requests.QueryHandlers
{
    public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdQuery, Movie>
    {
        private readonly PostgreContext _context;

        public GetMovieByIdHandler(PostgreContext context)
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
}