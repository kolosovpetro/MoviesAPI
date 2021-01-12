using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Commands.Commands;
using CqrsApi.Data.Context;
using CqrsApi.Responses.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsApi.Commands.Handlers
{
    public class DeleteMovieHandler : IRequestHandler<DeleteMovieCommand, DeleteMovieSuccessResponse>
    {
        private readonly PostgreContext _context;

        public DeleteMovieHandler(PostgreContext context)
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
}