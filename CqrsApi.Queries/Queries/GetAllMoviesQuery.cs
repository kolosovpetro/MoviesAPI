using System.Collections.Generic;
using CqrsApi.Models.Models;
using MediatR;

namespace CqrsApi.Queries.Queries
{
    public class GetAllMoviesQuery : IRequest<IList<Movie>>
    {
    }
}