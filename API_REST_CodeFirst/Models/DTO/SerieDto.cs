namespace API_REST_CodeFirst.Models.DTO
{
    public class SerieDto
    {
        public int SerieId { get; set; }
        public string Titre { get; set; } = null!;
        public int? AnneeCreation { get; set; }
        public string? Network { get; set; }
    }
}