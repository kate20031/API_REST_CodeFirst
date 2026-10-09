
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_CodeFirst.Models.EntityFramework
{
    [Table("serie")]
    public class Serie
    {
        [Key]
        [Column("serieid")]
        public int SerieId { get; set; }

        [Required]
        [Column("titre", TypeName = "varchar(100)")]
        public string Titre { get; set; } = null!;

        [Column("resume", TypeName = "text")]
        public string? Resume { get; set; }

        [Column("nbsaisons")]
        public int? NbSaisons { get; set; }

        [Column("nbepisodes")]
        public int? NbEpisodes { get; set; }

        [Column("anneecreation")]
        public int? AnneeCreation { get; set; }

        [Column("network", TypeName = "varchar(50)")]
        public string? Network { get; set; }
    }
}
