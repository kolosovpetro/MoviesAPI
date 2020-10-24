using System.Threading.Tasks;
using CqrsApi.Commands.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CqrsApi.Abstractions
{
    public interface IMovieController
    {
        Task<IActionResult> GetAllMoviesQuery();

        Task<IActionResult> GetMovieByIdQuery(int movieId);
        Task<IActionResult> AddMovie(CreateMovieCommand command);
        // Task<IActionResult> UpdateMovie(UpdateMovieCommand command);
        // Task<IActionResult> DeleteMovie(DeleteMovieCommand command);
    }
}