using CqrsApi.Models.Models;
using MediatR;

namespace CqrsApi.Queries.Queries
{
    public class GetByIdQuery : IRequest<Movie>
    {
        public int Id { get; }

        public GetByIdQuery(int id)
        {
            Id = id;
        }
    }
}