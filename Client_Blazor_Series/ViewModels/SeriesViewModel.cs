
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
    private async Task LoadSeriesAsync(
        CancellationToken cancellationToken)
    {
        ErrorMessage = null;
        StatusMessage = "Chargement des séries...";

        try
        {
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

    [RelayCommand]
    private async Task ReloadSlowAsync(
        CancellationToken cancellationToken)
    {
        ErrorMessage = null;
        StatusMessage = "Chargement lent en cours...";

        try
        {
            await Task.Delay(5000, cancellationToken);

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
