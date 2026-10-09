using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Models.EntityFramework
{
    public class SeriesContext : DbContext
    {
        public SeriesContext(DbContextOptions<SeriesContext> options)
            : base(options)
        {
        }

        public DbSet<Serie> Series { get; set; } = null!;
    }
}