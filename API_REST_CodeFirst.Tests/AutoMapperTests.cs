using AutoMapper;
using API_REST_CodeFirst.Profiles;

namespace API_REST_CodeFirst.Tests
{
    public class AutoMapperTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MovieProfile>();
            });

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void SeriesAutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<API_REST_CodeFirst.Profiles.SeriesProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}