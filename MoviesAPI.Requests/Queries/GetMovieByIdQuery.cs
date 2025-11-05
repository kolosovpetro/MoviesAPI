using MediatR;
using MoviesAPI.Models.Models;

namespace MoviesAPI.Requests.Queries;

public class GetMovieByIdQuery : IRequest<Movie>
{
    public int Id { get; }

    public GetMovieByIdQuery(int id)
    {
        Id = id;
    }
}