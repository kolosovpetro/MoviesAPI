using System.Collections.Generic;
using MediatR;
using MoviesAPI.Models.Models;

namespace MoviesAPI.Requests.Queries;

public class GetAllMoviesQuery : IRequest<IList<Movie>>
{
}