using AutoMapper;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.DTO;

namespace API_REST_CodeFirst.Profiles
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Serie, SerieDto>();

            CreateMap<Serie, SerieDetailDto>()
                .ForMember(
                    dest => dest.AverageEpisodesPerSeason,
                    opt => opt.MapFrom(src =>
                        (src.NbSaisons.HasValue &&
                         src.NbSaisons > 0 &&
                         src.NbEpisodes.HasValue)
                            ? Math.Round(
                                (double)src.NbEpisodes.Value / src.NbSaisons.Value,
                                2)
                            : 0.0))
                .ForMember(
                    dest => dest.EstEnCours,
                    opt => opt.MapFrom(src =>
                        src.AnneeCreation.HasValue &&
                        src.AnneeCreation >= 2024));
        }
    }
}