using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Data.Context;
using CqrsApi.Models.Models;
using CqrsApi.Queries.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsApi.Queries.Handlers
{
    public class GetAllMoviesHandler : IRequestHandler<GetAllMoviesQuery, IList<Movie>>
    {
        private readonly PostgreContext _context;

        public GetAllMoviesHandler(PostgreContext context)
        {
            _context = context;
        }

        public async Task<IList<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Movies.ToListAsync(cancellationToken);
        }
    }
}