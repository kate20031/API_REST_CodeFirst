using API_REST_CodeFirst.Models.DTO;

namespace API_REST_CodeFirst.Models.DataManager
{
    public interface ISerieManager
    {
        IEnumerable<SerieDto> GetSeries();
        SerieDetailDto? GetSerieById(int id);
        IEnumerable<NetworkDto> GetNetworks();
    }
}