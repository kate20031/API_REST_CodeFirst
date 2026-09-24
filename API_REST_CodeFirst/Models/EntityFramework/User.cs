using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_CodeFirst.Models.EntityFramework
{
    [Table("t_e_utilisateur_utl")]
    public class User
    {

        [Key]
        [Column("utl_id")]
        public int  UserId { get; set; }

        [Column("utl_nom",  TypeName = "varchar(50)")]
        public string? Name { get; set; }

        [Column("utl_prenom", TypeName = "varchar(50)")]
        public string? FirstName { get;  set; }


        [RegularExpression(@"^0[1-9][0-9]{8}$")]
        [Column("utl_mobile", TypeName = "char(10)")]
        public string? Mobile { get; set; }

        [Required]
        [EmailAddress]
        [Column("utl_mail", TypeName = "varchar(100)")]
        public string Mail { get; set; } = null!;

        [Required]
        [StringLength(64)]
        [Column("utl_pwd", TypeName = "varchar(64)")]
        public string Pwd { get; set; } = null!;

        [Column("utl_rue", TypeName = "varchar(200)")]
        public string? Street { get; set; }

        [Column("utl_cp", TypeName = "char(5)")]
        public string? Postcode { get;  set; }

        [Column("utl_ville", TypeName = "varchar(50)")]
        public string? City { get; set; }

        [Column("utl_pays", TypeName = "varchar(50)")]
        public string? Country { get; set; }

        [Column("utl_latitude", TypeName = "real")]
        public  float? Latitude { get; set; }

        [Column("utl_longitude", TypeName = "real")]
        public float? Longitude { get; set; }

        [Column("utl_datecreation", TypeName = "date")]


        public DateTime DateCreation {  get; set; }

        public ICollection<Rating> UserRatings { get; set; } = new List<Rating>();
    }
}