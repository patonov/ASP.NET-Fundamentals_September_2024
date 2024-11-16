using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCinemaMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CinemasMovies",
                columns: new[] { "CinemaId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumns: new[] { "CinemaId", "MovieId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumns: new[] { "CinemaId", "MovieId" },
                keyValues: new object[] { 2, 2 });
        }
    }
}
