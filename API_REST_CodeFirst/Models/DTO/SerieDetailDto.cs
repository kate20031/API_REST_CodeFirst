namespace API_REST_CodeFirst.Models.DTO
{
    public class SerieDetailDto
    {
        public int SerieId { get; set; }
        public string Titre { get; set; } = null!;
        public string? Resume { get; set; }
        public int? NbSaisons { get; set; }
        public int? NbEpisodes { get; set; }
        public int? AnneeCreation { get; set; }
        public string? Network { get; set; }

        public double AverageEpisodesPerSeason { get; set; }
        public bool EstEnCours { get; set; }
    }
}