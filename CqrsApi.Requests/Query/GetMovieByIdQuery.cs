using CqrsApi.Models.Models;
using MediatR;

namespace CqrsApi.Requests.Query
{
    public class GetMovieByIdQuery : IRequest<Movie>
    {
        public int Id { get; }

        public GetMovieByIdQuery(int id)
        {
            Id = id;
        }
    }
}