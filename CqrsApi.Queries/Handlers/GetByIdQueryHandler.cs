using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Data.Context;
using CqrsApi.Models.Models;
using CqrsApi.Queries.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsApi.Queries.Handlers
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Movie>
    {
        private readonly PostgreContext _context;

        public GetByIdQueryHandler(PostgreContext context)
        {
            _context = context;
        }

        public async Task<Movie> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context
                .Movies
                .FirstOrDefaultAsync(x => x.MovieId == request.Id, cancellationToken);
        }
    }
}