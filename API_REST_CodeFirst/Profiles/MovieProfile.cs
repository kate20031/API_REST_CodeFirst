using AutoMapper;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.DTO;

namespace API_REST_CodeFirst.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>();

            CreateMap<Movie, MovieDetailDto>();
        }
    }
}