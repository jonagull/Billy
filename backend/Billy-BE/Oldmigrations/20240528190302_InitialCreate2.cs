using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billy_BE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlayedMultiplePlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeOfPlay = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayedMultiplePlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    GamesPlayed = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Wins = table.Column<int>(type: "INTEGER", nullable: false),
                    Losses = table.Column<int>(type: "INTEGER", nullable: false),
                    Winrate = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentWinStreak = table.Column<int>(type: "INTEGER", nullable: false),
                    LongestWinStreak = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamesPlayed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerOneElo = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerTwoElo = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeOfPlay = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlayerOneId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerTwoId = table.Column<int>(type: "INTEGER", nullable: false),
                    WinnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPlayed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesPlayed_Players_PlayerOneId",
                        column: x => x.PlayerOneId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesPlayed_Players_PlayerTwoId",
                        column: x => x.PlayerTwoId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesPlayed_Players_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EloPre = table.Column<int>(type: "INTEGER", nullable: false),
                    EloPost = table.Column<int>(type: "INTEGER", nullable: false),
                    EloChange = table.Column<int>(type: "INTEGER", nullable: false),
                    Place = table.Column<int>(type: "INTEGER", nullable: false),
                    SnapshotTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    GamePlayedMultiplePlayersId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerSnapshots_GamePlayedMultiplePlayers_GamePlayedMultiplePlayersId",
                        column: x => x.GamePlayedMultiplePlayersId,
                        principalTable: "GamePlayedMultiplePlayers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerSnapshots_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlayed_PlayerOneId",
                table: "GamesPlayed",
                column: "PlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlayed_PlayerTwoId",
                table: "GamesPlayed",
                column: "PlayerTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlayed_WinnerId",
                table: "GamesPlayed",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSnapshots_GamePlayedMultiplePlayersId",
                table: "PlayerSnapshots",
                column: "GamePlayedMultiplePlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSnapshots_PlayerId",
                table: "PlayerSnapshots",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesPlayed");

            migrationBuilder.DropTable(
                name: "PlayerSnapshots");

            migrationBuilder.DropTable(
                name: "GamePlayedMultiplePlayers");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
