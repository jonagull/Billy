using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billy_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsToPlayerObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentWinStreak",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LongestWinStreak",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentWinStreak",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LongestWinStreak",
                table: "Players");
        }
    }
}
