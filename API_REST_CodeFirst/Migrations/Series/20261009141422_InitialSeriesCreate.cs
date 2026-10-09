using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_REST_CodeFirst.Migrations.Series
{
    /// <inheritdoc />
    public partial class InitialSeriesCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "serie",
                columns: table => new
                {
                    serieid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    titre = table.Column<string>(type: "varchar(100)", nullable: false),
                    resume = table.Column<string>(type: "text", nullable: true),
                    nbsaisons = table.Column<int>(type: "integer", nullable: true),
                    nbepisodes = table.Column<int>(type: "integer", nullable: true),
                    anneecreation = table.Column<int>(type: "integer", nullable: true),
                    network = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serie", x => x.serieid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serie");
        }
    }
}
