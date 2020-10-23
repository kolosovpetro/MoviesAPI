using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CqrsApi.Abstractions;
using CqrsApi.Queries.Queries;
using CqrsApi.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesQuery()
        {
            var request = new GetAllMoviesQuery();
            var response = await _mediator.Send(request);
            var mappedResponse = _mapper.Map<IList<MovieGetResponse>>(response);
            return mappedResponse != null ? (IActionResult) Ok(mappedResponse) : NotFound();
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<IActionResult> GetMovieByIdQuery(int id)
        {
            var query = new GetMovieByIdQuery(id);
            var response = await _mediator.Send(query);
            var mappedResponse = _mapper.Map<MovieGetResponse>(response);
            return response != null ? (IActionResult) Ok(mappedResponse) : NotFound();
        }
    }
}