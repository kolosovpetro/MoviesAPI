using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MoviesAPI.Data.Context;
using MoviesAPI.Models.Models;
using MoviesAPI.Requests.CommandResponses;
using MoviesAPI.Requests.Commands;

namespace MoviesAPI.Requests.CommandHandlers
{
    public class PostMovieHandler : IRequestHandler<PostMovieCommand, PostMovieSuccessResponse>
    {
        private readonly MoviesContext _context;

        public PostMovieHandler(MoviesContext context)
        {
            _context = context;
        }

        public async Task<PostMovieSuccessResponse> Handle(PostMovieCommand request,
            CancellationToken cancellationToken)
        {
            var movie = new Movie
            {
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