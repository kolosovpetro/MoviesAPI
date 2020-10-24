using AutoMapper;
using CqrsApi.Dto.Dto;
using CqrsApi.Models.Models;

namespace CqrsApi.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // source -> target
            CreateMap<Movie, MovieGetResponse>();
            CreateMap<AddMovieRequestDto, Movie>();
        }
    }
}