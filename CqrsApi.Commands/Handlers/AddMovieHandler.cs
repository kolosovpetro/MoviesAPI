using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Commands.Commands;
using CqrsApi.Data.Context;
using CqrsApi.Models.Models;
using MediatR;

namespace CqrsApi.Commands.Handlers
{
    public class AddMovieHandler : IRequestHandler<AddMovieCommand, Movie>
    {
        private readonly PostgreContext _context;

        public AddMovieHandler(PostgreContext context)
        {
            _context = context;
        }

        public async Task<Movie> Handle(AddMovieCommand request, CancellationToken cancellationToken)
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
            return movie;
        }
    }
}