using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billy_BE.Migrations
{
    /// <inheritdoc />
    public partial class WinStreakMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Use SQL logic to populate the win streak data
                -- This example uses SQLite-compatible syntax

                UPDATE Players
                SET CurrentWinStreak = 0, LongestWinStreak = 0;

                UPDATE Players AS p
                SET CurrentWinStreak = (
                  SELECT COUNT(*) FROM GamesPlayed 
                  WHERE WinnerId = p.Id AND TimeOfPlay > (
                    SELECT TimeOfPlay FROM GamesPlayed 
                    WHERE WinnerId != p.Id AND (PlayerOneId = p.Id OR PlayerTwoId = p.Id) 
                    ORDER BY TimeOfPlay DESC LIMIT 1
                  )
                ),
                LongestWinStreak = IFNULL((
                  SELECT COUNT(StreakId)
                  FROM (
                    SELECT WinnerId, TimeOfPlay,
                    ROW_NUMBER() OVER (ORDER BY TimeOfPlay) * 987 - 
                    ROW_NUMBER() OVER (PARTITION BY WinnerId ORDER BY TimeOfPlay) * 987 AS StreakId
                    FROM GamesPlayed WHERE PlayerOneId = p.Id OR PlayerTwoId = p.Id
                  ) AS Streaks 
                  WHERE WinnerId = p.Id
                  GROUP BY WinnerId, StreakId
                  ORDER BY COUNT(StreakId) DESC
                  LIMIT 1
                ), 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
