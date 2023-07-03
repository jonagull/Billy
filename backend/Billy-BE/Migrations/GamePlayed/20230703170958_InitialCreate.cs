﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billy_BE.Migrations.GamePlayed
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamesPlayed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerOneId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerOneElo = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerTwoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerTwoElo = table.Column<int>(type: "INTEGER", nullable: false),
                    WinnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeOfPlay = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPlayed", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesPlayed");
        }
    }
}
