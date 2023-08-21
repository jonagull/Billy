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

                WITH GameWinners AS (
                    SELECT
                        CASE WHEN g.PlayerOneId = p.Id THEN g.PlayerOneId ELSE g.PlayerTwoId END AS WinnerId,
                        g.TimeOfPlay
                    FROM
                        GamesPlayed g
                        JOIN Players p ON g.WinnerId = p.Id
                )
                UPDATE Players
                SET
                    CurrentWinStreak = (
                        SELECT COUNT(*) 
                        FROM (
                            SELECT MAX(TimeOfPlay) as MaxTimeOfPlay
                            FROM GameWinners
                            WHERE WinnerId = Players.Id
                            GROUP BY strftime('%Y-%m-%d', TimeOfPlay)
                        ) AS WinStreaks
                        WHERE WinStreaks.MaxTimeOfPlay = (SELECT MAX(TimeOfPlay) FROM GameWinners)
                    ),
                    LongestWinStreak = (
                        SELECT MAX(StreakCount) FROM (
                            SELECT
                                MAX(TimeOfPlay) AS MaxTimeOfPlay,
                                COUNT(*) AS StreakCount
                            FROM GameWinners
                            WHERE WinnerId = Players.Id
                            GROUP BY strftime('%Y-%m-%d', TimeOfPlay)
                        ) AS WinStreaks
                    )
                WHERE Id IN (SELECT DISTINCT WinnerId FROM GameWinners);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
