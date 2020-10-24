using CqrsApi.Models.Models;
using MediatR;

namespace CqrsApi.Commands.Commands
{
    // POST
    public class AddMovieCommand : IRequest<Movie>
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }
    }
}