using AutoMapper;
using MoviesAPI.Models.Models;
using MoviesAPI.Requests.QueryResponses;

namespace MoviesAPI.Core.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // source -> target
            CreateMap<Movie, GetMovieResponse>();
        }
    }
}