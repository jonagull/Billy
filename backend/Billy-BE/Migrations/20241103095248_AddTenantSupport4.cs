using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billy_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantSupport4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SnapshotTime",
                table: "PlayerSnapshots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SnapshotTime",
                table: "PlayerSnapshots",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
