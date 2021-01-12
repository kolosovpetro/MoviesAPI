using System.Threading.Tasks;
using CqrsApi.Requests.Command;
using Microsoft.AspNetCore.Mvc;

namespace CqrsApi.Core.Abstractions
{
    public interface IMovieController
    {
        Task<IActionResult> GetAllMoviesAsync();

        Task<IActionResult> GetMovieByIdAsync(int movieId);
        Task<IActionResult> PostMovieAsync(PostMovieCommand command);
        Task<IActionResult> PatchMovieAsync(PatchMovieCommand command);
        Task<IActionResult> DeleteMovieAsync(int id);
    }
}