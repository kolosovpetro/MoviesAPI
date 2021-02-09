using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CqrsApi.Core.Profiles;
using CqrsApi.Models.Models;
using CqrsApi.Requests.QueryResponses;

namespace CqrsApi.Tests.Controller
{
    public static class TestHelper
    {
        public static Task<IList<GetMovieResponse>> MappedMovies(IList<Movie> collection, IMapperBase mapper)
        {
            return Task.FromResult(mapper.Map<IList<GetMovieResponse>>(collection));
        }

        public static readonly IList<Movie> AllMovies = new List<Movie>
        {
            new Movie(1, "Star Wars Episode IV: A New Hope", 1979, 12, 10f),
            new Movie(2, "Ghostbusters", 1984, 12, 5.5f),
            new Movie(3, "Terminator", 1984, 15, 8.5f),
            new Movie(4, "Taxi Driver", 1976, 17, 5f),
            new Movie(5, "Platoon", 1986, 18, 5f),
            new Movie(6, "Frantic", 1988, 15, 8.5f),
            new Movie(7, "Ronin", 1998, 13, 9.5f),
            new Movie(8, "Analyze This", 1999, 16, 10.5f),
            new Movie(9, "Leon: the Professional", 1994, 16, 8.5f),
            new Movie(10, "Mission Impossible", 1996, 13, 8.5f)
        };

        private static readonly MapperProfile Profile = new MapperProfile();

        private static readonly MapperConfiguration Configuration = 
            new MapperConfiguration(cfg => cfg.AddProfile(Profile));

        public static readonly Mapper Mapper = new Mapper(Configuration);
    }
}