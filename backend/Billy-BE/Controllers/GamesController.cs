using Billy_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace Billy_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly PlayerContext _context;

        public GamesController(PlayerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult LogGame(GamePlayed game)
        {
            // Perform validation and error handling as needed
            try
            {
                // Find the players involved in the game by their IDs
                Player? playerOne = _context.Players.Find(game.PlayerOneId);
                Player? playerTwo = _context.Players.Find(game.PlayerTwoId);

                if (playerOne == null || playerTwo == null)
                {
                    return NotFound("One or both players not found");
                }

                playerOne.GamesPlayed++;
                playerTwo.GamesPlayed++;

                // Update the players' Elo ratings based on the game outcome
                UpdateEloRatings(playerOne, playerTwo, game.WinnerId);
                // Save changes to the database
                _context.SaveChanges();

                return Ok("Game logged successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private void UpdateEloRatings(Player? playerOne, Player? playerTwo, int winnerId)
        {
            // Calculate Elo rating changes based on the game outcome
            const int K = 32; // Elo rating adjustment constant

            if (playerTwo == null) return;
            
            double playerOneExpectedScore = 1 / (1 + Math.Pow(10, (playerTwo.Rating - playerOne.Rating) / 400.0));
            double playerTwoExpectedScore = 1 - playerOneExpectedScore;

            // Update Elo ratings based on the winner
            if (winnerId == playerOne.Id)
            {
                playerOne.Rating += (int)(K * (1 - playerOneExpectedScore));
                playerTwo.Rating += (int)(K * (0 - playerTwoExpectedScore));
            }
            else if (winnerId == playerTwo.Id)
            {
                playerOne.Rating += (int)(K * (0 - playerOneExpectedScore));
                playerTwo.Rating += (int)(K * (1 - playerTwoExpectedScore));
            }
            Console.WriteLine(playerOneExpectedScore);
            Console.WriteLine(playerTwoExpectedScore);
        }
    }
}