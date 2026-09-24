using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Models.EntityFramework
{
    public class CinemaContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=cinema;Username=postgres;Password=postgres"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rating>()
                .HasKey(r => new
                {
                    r.UserId,
                    r.FilmId
                });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Mail)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Country)
                .HasDefaultValue("France");

            modelBuilder.Entity<User>()
                .Property(u => u.DateCreation)
                .HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Rating>()
                .ToTable(t => t.HasCheckConstraint(
                    "ck_not_note",
                    "not_note >= 0 AND not_note <= 5"
                ));

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.MovieNote)
                .WithMany(m => m.NotesFilm)
                .HasForeignKey(r => r.FilmId)
                .HasConstraintName("fk_not_flm")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.UserNotator)
                .WithMany(u => u.UserRatings)
                .HasForeignKey(r => r.UserId)
                .HasConstraintName("fk_not_utl")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}