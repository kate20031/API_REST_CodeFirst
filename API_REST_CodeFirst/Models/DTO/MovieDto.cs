namespace API_REST_CodeFirst.Models.DTO
{
    public class MovieDto
    {
        public int FilmId { get; set; }
        public string Titre { get; set; } = null!;
        public DateTime DateRelease { get; set; }
        public decimal Duration { get; set; }
        public string? Gender { get; set; }
    }
}