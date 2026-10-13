using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client_Blazor_Series.Models.DTO;
using Client_Blazor_Series.Services;

namespace Client_Blazor_Series.ViewModels;

public partial class SeriesViewModel : ObservableObject
{
    private readonly ISerieService _serieService;

    [ObservableProperty]
    private List<SerieDto> series = new();

    [ObservableProperty]
    private string? errorMessage;

    [ObservableProperty]
    private string? statusMessage;

    public SeriesViewModel(ISerieService serieService)
    {
        _serieService = serieService;
    }

    [RelayCommand]
    private async Task LoadAsync(
        TimeSpan delay,
        CancellationToken cancellationToken)
    {
        ErrorMessage = null;
        StatusMessage = "Chargement des séries...";

        try
        {
            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, cancellationToken);
            }

            Series = await _serieService.GetSeriesAsync(
                cancellationToken);

            StatusMessage = $"{Series.Count} série(s) chargée(s).";
        }
        catch (OperationCanceledException)
        {
            StatusMessage = "Chargement annulé.";
        }
        catch (Exception)
        {
            ErrorMessage = "Impossible de charger les séries.";
            StatusMessage = null;
        }
    }
}
