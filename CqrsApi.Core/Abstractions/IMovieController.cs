using System.Threading.Tasks;
using CqrsApi.Commands.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CqrsApi.Abstractions
{
    public interface IMovieController
    {
        Task<IActionResult> GetAllMoviesAsync();

        Task<IActionResult> GetMovieByIdAsync(int movieId);
        Task<IActionResult> PostMovieAsync(CreateMovieCommand command);
        Task<IActionResult> PatchMovieAsync(UpdateMovieCommand command);
        Task<IActionResult> DeleteMovieAsync(int id);
    }
}