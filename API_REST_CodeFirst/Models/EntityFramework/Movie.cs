using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_CodeFirst.Models.EntityFramework
{
    [Table("t_e_film_flm")]
    public class Movie
    {
        [Key]
        [Column("flm_id")]
        public int FilmId { get; set; }


        [Column("flm_titre", TypeName = "varchar(50)")]
        [Required]
        public string Titre
        {
            get; set;
        }

        [Column("flm_resume", TypeName = "text")]
        public string Summary
        {
            get; set;
        }

        [Column("flm_datesortie", TypeName = "date")]
        public DateTime DateRelease
        {
            get; set;
        }

        [Column("flm_duree", TypeName = "numeric(3, 0)")]
        public decimal Duration
        {
            get; set;
        }

        [Column("flm_genre", TypeName = "varchar(30)")]
        public string Gender
        {
            get; set;
        }


        public ICollection<Rating> UserRatings { get; set; }
    }


}
