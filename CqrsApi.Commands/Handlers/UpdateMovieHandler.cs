using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Commands.Commands;
using CqrsApi.Data.Context;
using CqrsApi.Responses.Responses;
using MediatR;

namespace CqrsApi.Commands.Handlers
{
    public class UpdateMovieHandler : IRequestHandler<UpdateMovieCommand, UpdateSuccessResponse>
    {
        private readonly PostgreContext _context;

        public UpdateMovieHandler(PostgreContext context)
        {
            _context = context;
        }

        public async Task<UpdateSuccessResponse> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.MovieId == request.MovieId);
            if (movie == null)
                return null;

            movie.Title = request.Title;
            movie.Price = request.Price;
            movie.Year = request.Year;
            movie.AgeRestriction = request.AgeRestriction;
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateSuccessResponse(request.MovieId);
        }
    }
}