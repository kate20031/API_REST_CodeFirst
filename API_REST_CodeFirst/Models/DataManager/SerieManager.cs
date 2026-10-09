using AutoMapper;
using API_REST_CodeFirst.Models.DTO;
using API_REST_CodeFirst.Models.EntityFramework;

namespace API_REST_CodeFirst.Models.DataManager
{
    public class SerieManager : ISerieManager
    {
        private readonly SeriesContext _context;
        private readonly IMapper _mapper;

        public SerieManager(SeriesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<SerieDto> GetSeries()
        {
            var series = _context.Series.ToList();

            return _mapper.Map<IEnumerable<SerieDto>>(series);
        }

        public SerieDetailDto? GetSerieById(int id)
        {
            var serie = _context.Series.FirstOrDefault(s => s.SerieId == id);

            if (serie == null)
                return null;

            return _mapper.Map<SerieDetailDto>(serie);
        }

        public IEnumerable<NetworkDto> GetNetworks()
        {
            return _context.Series
                .GroupBy(s => s.Network)
                .Select(g => new NetworkDto
                {
                    NetworkName = g.Key ?? "Unknown",
                    NbSeries = g.Count(),
                    TotalEpisodes = g.Sum(s => s.NbEpisodes ?? 0)
                })
                .ToList();
        }
    }
}