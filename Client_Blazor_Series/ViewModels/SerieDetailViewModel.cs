
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client_Blazor_Series.Models.DTO;
using Client_Blazor_Series.Services;

namespace Client_Blazor_Series.ViewModels;

public partial class SerieDetailViewModel : ObservableObject
{
    private readonly ISerieService _serieService;

    [ObservableProperty]
    private SerieDetailDto? serie;

    [ObservableProperty]
    private string? errorMessage;

    [ObservableProperty]
    private string? statusMessage;

    public SerieDetailViewModel(ISerieService serieService)
    {
        _serieService = serieService;
    }

    [RelayCommand]
    private async Task LoadAsync(
        int id,
        CancellationToken cancellationToken)
    {
        ErrorMessage = null;
        StatusMessage = "Chargement de la série...";

        try
        {
            Serie = await _serieService.GetSerieByIdAsync(
                id, cancellationToken);

            if (Serie is null)
            {
                ErrorMessage = "Série introuvable.";
                StatusMessage = null;
                return;
            }

            StatusMessage = "Série chargée.";
        }
        catch (OperationCanceledException)
        {
            StatusMessage = "Chargement annulé.";
        }
        catch (Exception)
        {
            ErrorMessage = "Impossible de charger la série.";
            StatusMessage = null;
        }
    }

    [RelayCommand]
    private async Task SaveAsync(
        CancellationToken cancellationToken)
    {
        ErrorMessage = null;

        if (Serie is null)
        {
            ErrorMessage = "Aucune série à enregistrer.";
            return;
        }

        try
        {
            var success = await _serieService.UpdateSerieAsync(
                Serie.SerieId, Serie, cancellationToken);

            if (success)
            {
                StatusMessage = "Série enregistrée avec succès.";
            }
            else
            {
                ErrorMessage = "Impossible d'enregistrer la série.";
                StatusMessage = null;
            }
        }
        catch (OperationCanceledException)
        {
            StatusMessage = "Enregistrement annulé.";
        }
        catch (Exception)
        {
            ErrorMessage = "Une erreur est survenue lors de l'enregistrement.";
            StatusMessage = null;
        }
    }
}
