using AutoMapper;
using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.DTO;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Profiles;
using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Tests
{
    public class MovieManagerTests
    {
        private static CinemaContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<CinemaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CinemaContext(options);
        }

        private static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MovieProfile>();
            });

            return configuration.CreateMapper();
        }

        [Fact]
        public void GetAll_ReturnsMovieDtos()
        {
            using var context = CreateContext();

            context.Movies.Add(new Movie
            {
                FilmId = 1,
                Titre = "Test Movie",
                Summary = "Test summary",
                DateRelease = new DateTime(2020, 1, 1),
                Duration = 120,
                Gender = "Drama"
            });

            context.SaveChanges();

            var manager = new MovieManager(context, CreateMapper());

            var result = manager.GetAll().ToList();

            Assert.Single(result);
            Assert.Equal(1, result[0].FilmId);
            Assert.Equal("Test Movie", result[0].Titre);
        }

        [Fact]
        public void GetById_ExistingMovie_ReturnsMovieDetailDto()
        {
            using var context = CreateContext();

            context.Movies.Add(new Movie
            {
                FilmId = 1,
                Titre = "Test Movie",
                Summary = "Test summary",
                DateRelease = new DateTime(2020, 1, 1),
                Duration = 120,
                Gender = "Drama"
            });

            context.SaveChanges();

            var manager = new MovieManager(context, CreateMapper());

            var result = manager.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.FilmId);
            Assert.Equal("Test Movie", result.Titre);
            Assert.Equal("Test summary", result.Summary);
        }

        [Fact]
        public void GetById_NonExistingMovie_ReturnsNull()
        {
            using var context = CreateContext();

            var manager = new MovieManager(context, CreateMapper());

            var result = manager.GetById(999);

            Assert.Null(result);
        }
    }
}