using System.Threading.Tasks;
using CqrsApi.Queries.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CqrsApi.Abstractions
{
    public interface IMovieController
    {
        Task<IActionResult> GetAllMoviesQuery();

        Task<IActionResult> GetMovieByIdQuery(int movieId);
        // Task<IActionResult> AddMovie(AddMovieCommand command);
        // Task<IActionResult> UpdateMovie(UpdateMovieCommand command);
        // Task<IActionResult> DeleteMovie(DeleteMovieCommand command);
    }
}