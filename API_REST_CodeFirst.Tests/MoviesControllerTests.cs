using API_REST_CodeFirst.Controllers;
using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API_REST_CodeFirst.Tests
{
    public class MoviesControllerTests
    {
        [Fact]
        public void GetMovies_ReturnsMovieDtos()
        {
            var movies = new List<MovieDto>
            {
                new MovieDto
                {
                    FilmId = 1,
                    Titre = "Test Movie",
                    DateRelease = new DateTime(2020, 1, 1),
                    Duration = 120,
                    Gender = "Drama"
                }
            };

            var mockManager = new Mock<IMovieManager>();

            mockManager
                .Setup(x => x.GetAll())
                .Returns(movies);

            var controller = new MoviesController(mockManager.Object);

            var result = controller.GetMovies();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualMovies =
                Assert.IsAssignableFrom<IEnumerable<MovieDto>>(okResult.Value);

            Assert.Single(actualMovies);
            Assert.Equal("Test Movie", actualMovies.First().Titre);
        }

        [Fact]
        public void GetMovie_ExistingMovie_ReturnsMovieDetailDto()
        {
            var movie = new MovieDetailDto
            {
                FilmId = 1,
                Titre = "Test Movie",
                Summary = "Test summary",
                DateRelease = new DateTime(2020, 1, 1),
                Duration = 120,
                Gender = "Drama"
            };

            var mockManager = new Mock<IMovieManager>();

            mockManager
                .Setup(x => x.GetById(1))
                .Returns(movie);

            var controller = new MoviesController(mockManager.Object);

            var result = controller.GetMovie(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualMovie =
                Assert.IsType<MovieDetailDto>(okResult.Value);

            Assert.Equal(1, actualMovie.FilmId);
            Assert.Equal("Test Movie", actualMovie.Titre);
            Assert.Equal("Test summary", actualMovie.Summary);
        }

        [Fact]
        public void GetMovie_NonExistingMovie_ReturnsNotFound()
        {
            var mockManager = new Mock<IMovieManager>();

            mockManager
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((MovieDetailDto?)null);

            var controller = new MoviesController(mockManager.Object);

            var result = controller.GetMovie(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}