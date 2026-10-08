using AutoMapper;
using API_REST_CodeFirst.Models.DTO;
using API_REST_CodeFirst.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Models.DataManager
{
    public class MovieManager
    {
        private readonly CinemaContext _context;
        private readonly IMapper _mapper;

        public MovieManager(CinemaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<MovieDto> GetAll()
        {
            var movies = _context.Movies.ToList();

            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public MovieDetailDto? GetById(int id)
        {
            var movie = _context.Movies
                .FirstOrDefault(m => m.FilmId == id);

            if (movie == null)
            {
                return null;
            }

            return _mapper.Map<MovieDetailDto>(movie);
        }
    }
}