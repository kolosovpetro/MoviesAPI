using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Requests.Commands;

namespace MoviesAPI.Core.Abstractions;

public interface IMovieController
{
    Task<IActionResult> GetAllMoviesAsync();

    Task<IActionResult> GetMovieByIdAsync(int movieId);
    Task<IActionResult> PostMovieAsync(PostMovieCommand command);
    Task<IActionResult> PatchMovieAsync(PatchMovieCommand command);
    Task<IActionResult> DeleteMovieByIdAsync(int id);
}