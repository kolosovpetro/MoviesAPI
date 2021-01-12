using AutoMapper;
using CqrsApi.Models.Models;
using CqrsApi.Responses.Responses;

namespace CqrsApi.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // source -> target
            CreateMap<Movie, GetMovieByIdResponse>();
        }
    }
}