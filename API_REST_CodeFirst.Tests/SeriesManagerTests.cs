using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.EntityFramework;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace API_REST_CodeFirst.Tests
{
    public class SeriesManagerTests
    {
        private readonly IMapper _mapper;

        public SeriesManagerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<API_REST_CodeFirst.Profiles.SeriesProfile>();
            });

            _mapper = config.CreateMapper();
        }

        private SeriesContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<SeriesContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SeriesContext(options);
        }

        [Fact]
        public void GetNetworks_ReturnsCorrectAggregatedData()
        {
            using var context = CreateContext();

            context.Series.AddRange(
                new Serie
                {
                    SerieId = 1,
                    Titre = "Series A",
                    AnneeCreation = 2020,
                    Network = "Netflix",
                    NbEpisodes = 10,
                    NbSaisons = 2
                },
                new Serie
                {
                    SerieId = 2,
                    Titre = "Series B",
                    AnneeCreation = 2021,
                    Network = "Netflix",
                    NbEpisodes = 20,
                    NbSaisons = 4
                },
                new Serie
                {
                    SerieId = 3,
                    Titre = "Series C",
                    AnneeCreation = 2022,
                    Network = "HBO",
                    NbEpisodes = 15,
                    NbSaisons = 3
                }
            );

            context.SaveChanges();

            var manager = new SerieManager(context, _mapper);

            var result = manager.GetNetworks().ToList();

            var netflix = result.Single(n => n.NetworkName == "Netflix");
            var hbo = result.Single(n => n.NetworkName == "HBO");

            Assert.Equal(2, netflix.NbSeries);
            Assert.Equal(30, netflix.TotalEpisodes);

            Assert.Equal(1, hbo.NbSeries);
            Assert.Equal(15, hbo.TotalEpisodes);
        }
    }
}