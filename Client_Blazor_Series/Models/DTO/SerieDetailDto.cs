
using System.ComponentModel.DataAnnotations;

namespace Client_Blazor_Series.Models.DTO
{
    public class SerieDetailDto
    {
        public int SerieId { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères.")]
        public string Titre { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Le résumé ne peut pas dépasser 2000 caractères.")]
        public string? Resume { get; set; }

        [Range(0, 100, ErrorMessage = "Le nombre de saisons doit être compris entre 0 et 100.")]
        public int? NbSaisons { get; set; }

        [Range(0, 10000, ErrorMessage = "Le nombre d'épisodes doit être compris entre 0 et 10000.")]
        public int? NbEpisodes { get; set; }

        [Range(1800, 2100, ErrorMessage = "L'année doit être comprise entre 1800 et 2100.")]
        public int? AnneeCreation { get; set; }

        [StringLength(100, ErrorMessage = "Le nom du réseau ne peut pas dépasser 100 caractères.")]
        public string? Network { get; set; }

        public double AverageEpisodesPerSeason { get; set; }

        public bool EstEnCours { get; set; }
    }
}
