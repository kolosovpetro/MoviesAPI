using MediatR;
using MoviesAPI.Requests.CommandResponses;

namespace MoviesAPI.Requests.Commands;

public class PatchMovieCommand: IRequest<PatchMovieSuccessResponse>
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int AgeRestriction { get; set; }
    public float Price { get; set; }
}