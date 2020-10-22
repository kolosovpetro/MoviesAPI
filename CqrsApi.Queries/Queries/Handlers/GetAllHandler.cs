using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Data.Context;
using CqrsApi.Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsApi.Queries.Queries.Handlers
{
    public class GetAllHandler : IRequestHandler<GetAllQuery, IList<Movie>>
    {
        private readonly PostgreContext _context;

        public GetAllHandler(PostgreContext context)
        {
            _context = context;
        }

        public async Task<IList<Movie>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _context.Movies.ToListAsync(cancellationToken);
        }
    }
}