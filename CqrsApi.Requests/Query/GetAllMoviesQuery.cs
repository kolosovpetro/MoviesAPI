using System.Collections.Generic;
using CqrsApi.Models.Models;
using MediatR;

namespace CqrsApi.Requests.Query
{
    public class GetAllMoviesQuery : IRequest<IList<Movie>>
    {
    }
}