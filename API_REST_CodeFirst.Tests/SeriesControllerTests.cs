
using API_REST_CodeFirst.Controllers;
using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace API_REST_CodeFirst.Tests
{
    public class SeriesControllerTests
    {
        [Fact]
        public void GetSeries_ReturnsOkWithSeries()
        {
            var mockManager = new Mock<ISerieManager>();

            var series = new List<SerieDto>
            {
                new SerieDto
                {
                    SerieId = 1,
                    Titre = "Scrubs",
                    AnneeCreation = 2001,
                    Network = "ABC (US)"
                }
            };

            mockManager
                .Setup(m => m.GetSeries())
                .Returns(series);

            var controller = new SeriesController(mockManager.Object);

            var result = controller.GetSeries();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSeries =
                Assert.IsAssignableFrom<IEnumerable<SerieDto>>(okResult.Value);

            Assert.Single(returnedSeries);
            Assert.Equal("Scrubs", returnedSeries.First().Titre);
        }

        [Fact]
        public void GetSerie_ExistingId_ReturnsOkWithDetail()
        {
            var mockManager = new Mock<ISerieManager>();

            var serie = new SerieDetailDto
            {
                SerieId = 1,
                Titre = "Scrubs",
                Resume = "A medical comedy series",
                NbSaisons = 9,
                NbEpisodes = 184,
                AnneeCreation = 2001,
                Network = "ABC (US)",
                AverageEpisodesPerSeason = 20.44,
                EstEnCours = false
            };

            mockManager
                .Setup(m => m.GetSerieById(1))
                .Returns(serie);

            var controller = new SeriesController(mockManager.Object);

            var result = controller.GetSerie(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSerie =
                Assert.IsType<SerieDetailDto>(okResult.Value);

            Assert.Equal(1, returnedSerie.SerieId);
            Assert.Equal("Scrubs", returnedSerie.Titre);
            Assert.Equal(20.44, returnedSerie.AverageEpisodesPerSeason);
        }

        [Fact]
        public void GetSerie_UnknownId_ReturnsNotFound()
        {
            var mockManager = new Mock<ISerieManager>();

            mockManager
                .Setup(m => m.GetSerieById(999))
                .Returns((SerieDetailDto?)null);

            var controller = new SeriesController(mockManager.Object);

            var result = controller.GetSerie(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetNetworks_ReturnsOkWithNetworks()
        {
            var mockManager = new Mock<ISerieManager>();

            var networks = new List<NetworkDto>
            {
                new NetworkDto
                {
                    NetworkName = "Netflix",
                    NbSeries = 2,
                    TotalEpisodes = 30
                },
                new NetworkDto
                {
                    NetworkName = "HBO",
                    NbSeries = 1,
                    TotalEpisodes = 15
                }
            };

            mockManager
                .Setup(m => m.GetNetworks())
                .Returns(networks);

            var controller = new SeriesController(mockManager.Object);

            var result = controller.GetNetworks();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedNetworks =
                Assert.IsAssignableFrom<IEnumerable<NetworkDto>>(okResult.Value);

            Assert.Equal(2, returnedNetworks.Count());

            var netflix =
                returnedNetworks.Single(n => n.NetworkName == "Netflix");

            Assert.Equal(2, netflix.NbSeries);
            Assert.Equal(30, netflix.TotalEpisodes);
        }

        [Fact]
        public void UpdateSerie_ValidSerie_ReturnsNoContent()
        {
            var mockManager = new Mock<ISerieManager>();

            var serie = new SerieDetailDto
            {
                SerieId = 1,
                Titre = "Scrubs Updated",
                Resume = "Updated description",
                NbSaisons = 9,
                NbEpisodes = 184,
                AnneeCreation = 2001,
                Network = "ABC (US)"
            };

            mockManager
                .Setup(m => m.UpdateSerie(1, serie))
                .Returns(true);

            var controller = new SeriesController(mockManager.Object);

            var result = controller.UpdateSerie(1, serie);

            Assert.IsType<NoContentResult>(result);
            mockManager.Verify(m => m.UpdateSerie(1, serie), Times.Once);
        }

        [Fact]
        public void UpdateSerie_UnknownId_ReturnsNotFound()
        {
            var mockManager = new Mock<ISerieManager>();

            var serie = new SerieDetailDto
            {
                SerieId = 999,
                Titre = "Unknown series"
            };

            mockManager
                .Setup(m => m.UpdateSerie(999, serie))
                .Returns(false);

            var controller = new SeriesController(mockManager.Object);

            var result = controller.UpdateSerie(999, serie);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateSerie_MismatchedIds_ReturnsBadRequest()
        {
            var mockManager = new Mock<ISerieManager>();

            var serie = new SerieDetailDto
            {
                SerieId = 2,
                Titre = "Scrubs"
            };

            var controller = new SeriesController(mockManager.Object);

            var result = controller.UpdateSerie(1, serie);

            Assert.IsType<BadRequestObjectResult>(result);

            mockManager.Verify(
                m => m.UpdateSerie(
                    It.IsAny<int>(),
                    It.IsAny<SerieDetailDto>()),
                Times.Never);
        }
    }
}
