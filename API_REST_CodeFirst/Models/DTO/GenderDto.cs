namespace API_REST_CodeFirst.Models.DTO
{
    public class GenderDto
    {
        public string GenderName { get; set; } = null!;
        public int NbMovies { get; set; }
        public int TotalRatings { get; set; }
    }
}