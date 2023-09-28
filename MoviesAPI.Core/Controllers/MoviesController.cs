using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Core.Abstractions;
using MoviesAPI.Requests.CommandResponses;
using MoviesAPI.Requests.Commands;
using MoviesAPI.Requests.Queries;
using MoviesAPI.Requests.QueryResponses;
using MoviesAPI.Requests.ValidationResponses;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Core.Controllers
{
    [ApiController]
    [Route("api/movies")]
    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
    public class MoviesController : Controller, IMovieController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private const string
            SectionName = "Application"; // it is reference to the Application event log section, not app name

        private const string SourceName = "EventLogEntryDemo.API.SourceNew";

        public MoviesController(IMediator mediator, IMapper mapper)
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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WriteToWindowsEventLog("GetAllMoviesAsync Invoked", EventLogEntryType.Information);

            var request = new GetAllMoviesQuery();
            var response = await _mediator.Send(request);

            if (response == null)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WriteToWindowsEventLog("GetAllMoviesAsync Not Found", EventLogEntryType.Error);
                return NotFound();
            }

            var mappedResponse = _mapper.Map<IList<GetMovieResponse>>(response);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WriteToWindowsEventLog("GetAllMoviesAsync Success", EventLogEntryType.Information);

            return mappedResponse != null
                ? Ok(mappedResponse)
                : NotFound();
        }

        /// <summary>
        /// Returns movie by id.
        /// </summary>
        [HttpGet("{id}", Name = "GetMovieByIdAsync")]
        [SwaggerOperation(Summary = "Returns movie by id.")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WriteToWindowsEventLog("GetMovieByIdAsync Invoked", EventLogEntryType.Information);
            }

            if (id < 0)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WriteToWindowsEventLog($"GetMovieByIdAsync Not found {id}", EventLogEntryType.Error);
                return BadRequest(new InvalidIdResponse());
            }

            var query = new GetMovieByIdQuery(id);
            var response = await _mediator.Send(query);

            if (response == null)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WriteToWindowsEventLog($"GetMovieByIdAsync Not found {id}", EventLogEntryType.Error);
                return NotFound(new MovieNotFoundResponse(id));
            }

            var mappedResponse = _mapper.Map<GetMovieResponse>(response);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WriteToWindowsEventLog("GetMovieByIdAsync Success", EventLogEntryType.Information);

            return Ok(mappedResponse);
        }

        /// <summary>
        /// Adds new movie to database. Returns response.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Adds new movie to database. Returns response.")]
        public async Task<IActionResult> PostMovieAsync(PostMovieCommand command)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WriteToWindowsEventLog("PostMovieAsync Invoked", EventLogEntryType.Information);

            if (command.Year < 1888)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WriteToWindowsEventLog($"PostMovieAsync Year {command.Year}", EventLogEntryType.Error);
                return BadRequest(new InvalidYearResponse());
            }

            if (command.Price <= 0)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WriteToWindowsEventLog($"PostMovieAsync Price {command.Price}", EventLogEntryType.Error);
                return BadRequest(new InvalidPriceResponse());
            }

            if (command.AgeRestriction <= 0)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WriteToWindowsEventLog($"PostMovieAsync AgeRestriction {command.AgeRestriction}",
                        EventLogEntryType.Error);
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

        private static void WriteToWindowsEventLog(string message, EventLogEntryType type)
        {
            var appLog = new EventLog(SectionName);
            var sourceName = CreateEventSource(SectionName);
            appLog.Source = sourceName;

            using var eventLog = new EventLog(SectionName);
            eventLog.Source = sourceName;
            eventLog.WriteEntry(message, type);
        }

        private static string CreateEventSource(string currentAppName)
        {
            var sourceExists = EventLog.SourceExists(SourceName);

            if (!sourceExists)
            {
                EventLog.CreateEventSource(SourceName, currentAppName);
            }

            return SourceName;
        }
    }
}