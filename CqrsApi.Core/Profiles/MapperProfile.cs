using AutoMapper;
using CqrsApi.Models.Models;
using CqrsApi.Queries.Responses;

namespace CqrsApi.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Movie, MovieGetResponse>();
        }
    }
}