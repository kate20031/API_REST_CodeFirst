using AutoMapper;
using API_REST_CodeFirst.Models.DTO;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Profiles;

namespace API_REST_CodeFirst.Tests
{
    public class MovieDetailMappingTests
    {
        [Fact]
        public void Movie_Should_Map_To_MovieDetailDto()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MovieProfile>();
            });

            var mapper = configuration.CreateMapper();

            var movie = new Movie
            {
                FilmId = 1,
                Titre = "just some test movie",
                Summary = "Test summary",
                DateRelease = new DateTime(2020, 1, 1),
                Duration = 147,
                Gender = "Dramaa"
            };

            var dto = mapper.Map<MovieDetailDto>(movie);

            Assert.Equal(movie.FilmId, dto.FilmId);
            Assert.Equal(movie.Titre, dto.Titre);
            Assert.Equal(movie.Summary, dto.Summary);
            Assert.Equal(movie.DateRelease, dto.DateRelease);
            Assert.Equal(movie.Duration, dto.Duration);
            Assert.Equal(movie.Gender, dto.Gender);
        }
    }
}