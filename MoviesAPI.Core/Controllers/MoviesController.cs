using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesAPI.Core.Abstractions;
using MoviesAPI.Requests.CommandResponses;
using MoviesAPI.Requests.Commands;
using MoviesAPI.Requests.Queries;
using MoviesAPI.Requests.QueryResponses;
using MoviesAPI.Requests.ValidationResponses;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Core.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : Controller, IMovieController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(
        IMediator mediator,
        IMapper mapper,
        ILogger<MoviesController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Returns list of all the movies in database.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Returns list of all the movies in database.")]
    public async Task<IActionResult> GetAllMoviesAsync()
    {
        _logger.LogInformation("GetAllMoviesAsync Invoked.");

        var request = new GetAllMoviesQuery();
        var response = await _mediator.Send(request);


        if (response == null)
        {
            _logger.LogError("There are no movies in the Database.");

            return NotFound();
        }

        var mappedResponse = _mapper.Map<IList<GetMovieResponse>>(response);

        _logger.LogInformation("GetAllMoviesAsync: OK.");

        return Ok(mappedResponse);
    }

    /// <summary>
    /// Returns movie by id.
    /// </summary>
    [HttpGet("{id}", Name = "GetMovieByIdAsync")]
    [SwaggerOperation(Summary = "Returns movie by id.")]
    public async Task<IActionResult> GetMovieByIdAsync(int id)
    {
        _logger.LogInformation("GetMovieByIdAsync Invoked");


        if (id < 0)
        {
            _logger.LogError($"GetMovieByIdAsync invalid ID: {id}.");

            return BadRequest(new InvalidIdResponse());
        }

        var query = new GetMovieByIdQuery(id);
        var response = await _mediator.Send(query);

        if (response == null)
        {
            _logger.LogError($"GetMovieByIdAsync Not found {id}.");

            return NotFound(new MovieNotFoundResponse(id));
        }

        var mappedResponse = _mapper.Map<GetMovieResponse>(response);

        return Ok(mappedResponse);
    }

    /// <summary>
    /// Adds new movie to database. Returns response.
    /// </summary>
    [HttpPost]
    [SwaggerOperation(Summary = "Adds new movie to database. Returns response.")]
    public async Task<IActionResult> PostMovieAsync(PostMovieCommand command)
    {
        _logger.LogInformation("PostMovieAsync Invoked");

        if (command.Year < 1888)
        {
            _logger.LogError($"PostMovieAsync invalid year: {command.Year}");

            return BadRequest(new InvalidYearResponse());
        }

        if (command.Price <= 0)
        {
            _logger.LogInformation($"PostMovieAsync invalid price: {command.Price}");

            return BadRequest(new InvalidPriceResponse());
        }

        if (command.AgeRestriction <= 0)
        {
            _logger.LogInformation($"PostMovieAsync invalid age restriction: {command.AgeRestriction}");

            return BadRequest(new InvalidAgeRestrictionResponse());
        }

        var response = await _mediator.Send(command);

        return Ok(response);
    }

    /// <summary>
    /// Modifies an existing movie in database. Returns response.
    /// </summary>
    [HttpPatch]
    [SwaggerOperation(Summary = "Modifies an existing movie in database. Returns response.")]
    public async Task<IActionResult> PatchMovieAsync(PatchMovieCommand command)
    {
        var model = await _mediator.Send(new GetMovieByIdQuery(command.MovieId));

        if (model.Title.Contains("F#"))
        {
            return BadRequest("F# best language");
        }

        if (command.MovieId < 0)
        {
            return BadRequest(new InvalidIdResponse());
        }

        var response = await _mediator.Send(command);

        if (response == null)
        {
            return NotFound(new MovieNotFoundResponse(command.MovieId));
        }

        return Ok(new PatchMovieSuccessResponse(command.MovieId));
    }

    /// <summary>
    /// Deletes movie from database by id. Returns response.
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteMovieByIdAsync")]
    [SwaggerOperation(Summary = "Deletes movie from database by Id. Returns response.")]
    public async Task<IActionResult> DeleteMovieByIdAsync(int id)
    {
        var model = await _mediator.Send(new GetMovieByIdQuery(id));

        if (model.Title.Contains("F#"))
        {
            return BadRequest("F# best language");
        }

        var command = new DeleteMovieCommand(id);
        var response = await _mediator.Send(command);

        if (response == null)
        {
            return NotFound(new MovieNotFoundResponse(command.MovieId));
        }

        return Ok(response);
    }
}