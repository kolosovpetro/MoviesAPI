using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CqrsApi.Abstractions;
using CqrsApi.Commands.Commands;
using CqrsApi.Queries.Queries;
using CqrsApi.Responses.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CqrsApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : Controller, IMovieController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MovieController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns list of all the movies in database.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Returns list of all the movies in database.")]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var request = new GetAllMoviesQuery();
            var response = await _mediator.Send(request);
            if (response == null)
            {
                return NotFound();
            }
            var mappedResponse = _mapper.Map<IList<MovieGetResponse>>(response);
            return mappedResponse != null ? (IActionResult) Ok(mappedResponse) : NotFound();
        }

        /// <summary>
        /// Returns movie by Id.
        /// </summary>
        [HttpGet("{id}", Name = "GetMovieByIdAsync")]
        [SwaggerOperation(Summary = "Returns movie by Id.")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            if (id < 0)
                return BadRequest(new InvalidIdResponse());
            
            var query = new GetMovieByIdQuery(id);
            var response = await _mediator.Send(query);
            if (response == null)
                return NotFound(new MovieNotFoundResponse(id));

            var mappedResponse = _mapper.Map<MovieGetResponse>(response);
            return Ok(mappedResponse);
        }

        /// <summary>
        /// Adds new movie to database. Returns response with report.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Adds new movie to database. Returns response with report.")]
        public async Task<IActionResult> PostMovieAsync(CreateMovieCommand command)
        {
            if (command.Year < 1888)
                return BadRequest(new InvalidYearResponse());

            if (command.Price <= 0)
                return BadRequest(new InvalidPriceResponse());

            if (command.AgeRestriction <= 0)
                return BadRequest(new InvalidAgeRestrictionResponse());

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Modifies an existing movie in database. Returns response.
        /// </summary>
        [HttpPatch]
        [SwaggerOperation(Summary = "Modifies an existing movie in database. Returns response.")]
        public async Task<IActionResult> PatchMovieAsync(UpdateMovieCommand command)
        {
            if (command.MovieId < 0)
                return BadRequest(new InvalidIdResponse());

            var response = await _mediator.Send(command);

            if (response == null)
                return NotFound(new MovieNotFoundResponse(command.MovieId));

            return Ok(new UpdateSuccessResponse(command.MovieId));
        }

        /// <summary>
        /// Deletes movie from database by Id. Returns response.
        /// </summary>
        [HttpDelete]
        [SwaggerOperation(Summary = "Deletes movie from database by Id. Returns response.")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var command = new DeleteMovieCommand(id);
            var response = await _mediator.Send(command);
            if (response == null)
                return NotFound(new MovieNotFoundResponse(command.MovieId));

            return Ok(response);
        }
    }
}