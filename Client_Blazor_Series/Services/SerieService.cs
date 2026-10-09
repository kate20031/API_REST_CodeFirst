
using Client_Blazor_Series.Services;
using Client_Blazor_Series.Models.DTO;
using System.Net.Http.Json;

namespace ClientBlazorSeries.Services;

public class SerieService : ISerieService
{
    private readonly HttpClient _httpClient;

    public SerieService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SerieDto>> GetSeriesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<SerieDto>>(
            "api/Series", cancellationToken) ?? new List<SerieDto>();
    }

    public async Task<SerieDetailDto?> GetSerieByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<SerieDetailDto>(
            $"api/Series/{id}", cancellationToken);
    }

    public async Task<bool> UpdateSerieAsync(
        int id,
        SerieDetailDto serie,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync(
            $"api/Series/{id}", serie, cancellationToken);

        return response.IsSuccessStatusCode;
    }
}
