using AutoMapper;
using CqrsApi.Models.Models;
using CqrsApi.Requests.QueryResponses;

namespace CqrsApi.Core.Profiles
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