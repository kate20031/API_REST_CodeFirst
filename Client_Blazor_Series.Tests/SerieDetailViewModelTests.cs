
using Client_Blazor_Series.Models.DTO;
using Client_Blazor_Series.Services;
using Client_Blazor_Series.ViewModels;
using Moq;
using Xunit;

namespace Client_Blazor_Series.Tests
{
    public class SerieDetailViewModelTests
    {
        [Fact]
        public async Task LoadAsync_ExistingSerie_LoadsSerie()
        {
            var serie = new SerieDetailDto
            {
                SerieId = 1,
                Titre = "Scrubs",
                Resume = "Medical comedy",
                NbSaisons = 9,
                NbEpisodes = 182,
                AnneeCreation = 2001,
                Network = "NBC"
            };

            var mockService = new Mock<ISerieService>();

            mockService
                .Setup(s => s.GetSerieByIdAsync(
                    1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(serie);

            var viewModel = new SerieDetailViewModel(
                mockService.Object);

            await viewModel.LoadCommand.ExecuteAsync(1);

            Assert.NotNull(viewModel.Serie);
            Assert.Equal("Scrubs", viewModel.Serie!.Titre);
            Assert.Equal("Série chargée.", viewModel.StatusMessage);
            Assert.Null(viewModel.ErrorMessage);
        }

        [Fact]
        public async Task LoadAsync_UnknownSerie_SetsErrorMessage()
        {
            var mockService = new Mock<ISerieService>();

            mockService
                .Setup(s => s.GetSerieByIdAsync(
                    999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((SerieDetailDto?)null);

            var viewModel = new SerieDetailViewModel(
                mockService.Object);

            await viewModel.LoadCommand.ExecuteAsync(999);

            Assert.Null(viewModel.Serie);
            Assert.Equal("Série introuvable.", viewModel.ErrorMessage);
        }

        [Fact]
        public async Task SaveAsync_ValidSerie_SavesSerie()
        {
            var serie = new SerieDetailDto
            {
                SerieId = 1,
                Titre = "Scrubs Updated"
            };

            var mockService = new Mock<ISerieService>();

            mockService
                .Setup(s => s.UpdateSerieAsync(
                    1, serie, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var viewModel = new SerieDetailViewModel(
                mockService.Object);

            viewModel.Serie = serie;

            await viewModel.SaveCommand.ExecuteAsync(
                CancellationToken.None);

            Assert.Equal(
                "Série enregistrée avec succès.",
                viewModel.StatusMessage);

            Assert.Null(viewModel.ErrorMessage);

            mockService.Verify(
                s => s.UpdateSerieAsync(
                    1, serie, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
