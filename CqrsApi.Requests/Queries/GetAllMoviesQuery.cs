using System.Collections.Generic;
using CqrsApi.Models.Models;
using MediatR;

namespace CqrsApi.Requests.Queries
{
    public class GetAllMoviesQuery : IRequest<IList<Movie>>
    {
    }
}