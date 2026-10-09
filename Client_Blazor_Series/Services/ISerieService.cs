
using Client_Blazor_Series.Models.DTO;
using Client_Blazor_Series.Models.DTO;

namespace Client_Blazor_Series.Services;

public interface ISerieService
{
    Task<List<SerieDto>> GetSeriesAsync(
        CancellationToken cancellationToken = default);

    Task<SerieDetailDto?> GetSerieByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateSerieAsync(
        int id,
        SerieDetailDto serie,
        CancellationToken cancellationToken = default);
}
