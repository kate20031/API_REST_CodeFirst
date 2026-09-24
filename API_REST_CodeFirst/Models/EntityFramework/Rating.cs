using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_CodeFirst.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public class Rating
    {
        [Column("utl_id")]
        public int UserId { get; set; }

        [Column("flm_id")]
        public int FilmId { get; set; }

        [Required]
        [Column("not_note")]
        [Range(0, 5)]
        public int Note { get; set; }

        public User UserNotator { get; set; } = null!;

        public Movie MovieNote { get; set; } = null!;
    }
}