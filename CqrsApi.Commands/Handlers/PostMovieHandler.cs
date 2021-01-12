using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Commands.Commands;
using CqrsApi.Data.Context;
using CqrsApi.Models.Models;
using CqrsApi.Responses.Responses;
using MediatR;

namespace CqrsApi.Commands.Handlers
{
    public class PostMovieHandler : IRequestHandler<PostMovieCommand, PostMovieSuccessResponse>
    {
        private readonly PostgreContext _context;

        public PostMovieHandler(PostgreContext context)
        {
            _context = context;
        }

        public async Task<PostMovieSuccessResponse> Handle(PostMovieCommand request,
            CancellationToken cancellationToken)
        {
            var movieId = _context.Movies.Max(x => x.MovieId) + 1; // new id

            var movie = new Movie
            {
                MovieId = movieId,
                Title = request.Title,
                Year = request.Year,
                AgeRestriction = request.AgeRestriction,
                Price = request.Price
            };

            await _context.Movies.AddAsync(movie, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new PostMovieSuccessResponse(request.Title);
        }
    }
}