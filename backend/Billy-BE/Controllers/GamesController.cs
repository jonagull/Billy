using Billy_BE.DTOs;
using Billy_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly BillyContext _billyContext;

        public GamesController(BillyContext billyContext)
        {
            _billyContext = billyContext;
        }

        [HttpGet]
        public IActionResult GetAllGamesPlayed()
        {
            try
            {
                var gamesPlayed= _billyContext.GamesPlayed
                    .Include(game => game.PlayerOne)
                    .Include(game => game.PlayerTwo)
                    .Include(game => game.Winner)
                    .ToList();

                var gamesWithNames = gamesPlayed.Select(game => new
                {
                    game.Id,
                    PlayerOneName = game.PlayerOne.Name,
                    PlayerTwoName = game.PlayerTwo.Name,
                    WinnerName = game.Winner.Name,
                    TimeOfPlay = game.TimeOfPlay.AddHours(2)
                }).ToList();

                return Ok(gamesWithNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogGame(GamePlayedDto gameDto)
        {
            // Perform validation and error handling as needed
            try
            {
                // Find the players involved in the game by their IDs
                var playerOne = await _billyContext.Players.FindAsync(gameDto.PlayerOneId);
                var playerTwo = await _billyContext.Players.FindAsync(gameDto.PlayerTwoId);

                if (playerOne == null || playerTwo == null)
                {
                    return NotFound("One or both players not found");
                }

                var game = new GamePlayed(
                    playerOne,
                    playerTwo,
                    gameDto.WinnerId == gameDto.PlayerOneId ? playerOne : playerTwo);

                UpdatePlayerMetrics(playerOne, playerTwo, gameDto.WinnerId);
                // Update the players' Elo ratings based on the game outcome
                UpdateEloRatings(playerOne, playerTwo, gameDto.WinnerId);
                // Save changes to the database
                await _billyContext.SaveChangesAsync();
                
                _billyContext.Add(game);
                await _billyContext.SaveChangesAsync();
                
                int playerOneRatingDiff = playerOne.Rating - game.PlayerOneElo;
                int playerTwoRatingDiff = playerTwo.Rating - game.PlayerTwoElo;

                // Create a response object containing the updated player objects and rating differences
                var response = new
                {
                    PlayerOne = new
                    {
                        NewRating = playerOne.Rating,
                        RatingDiff = playerOneRatingDiff
                    },
                    PlayerTwo = new
                    {
                        NewRating = playerTwo.Rating,
                        RatingDiff = playerTwoRatingDiff
                    }
                };

                // Return the response as JSON
                return Ok(response);
            }
            
            
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        private void UpdatePlayerMetrics(Player playerOne, Player playerTwo, int winnerId)
        {
            playerOne.GamesPlayed++;
            playerTwo.GamesPlayed++;
            
            // Update the players' metrics based on the game outcome
            if (winnerId == playerOne.Id)
            {
                playerOne.Wins++;
                playerTwo.Losses++;
            }
            else if (winnerId == playerTwo.Id)
            {
                playerOne.Losses++;
                playerTwo.Wins++;
            }

            playerOne.Winrate = (int)(playerOne.Losses > 0 || playerOne.Wins > 0 ? (playerOne.Wins * 100.0 / playerOne.GamesPlayed) : 0);
            playerTwo.Winrate = (int)(playerTwo.Losses > 0 || playerTwo.Wins > 0? (playerTwo.Wins * 100.0 / playerTwo.GamesPlayed) : 0);

        }

        private void UpdateEloRatings(Player? playerOne, Player? playerTwo, int winnerId)
        {
            // Calculate Elo rating changes based on the game outcome
            const int K = 32; // Elo rating adjustment constant

            if (playerTwo == null) return;

            if (playerOne == null) return;
            var playerOneExpectedScore = 1 / (1 + Math.Pow(10, (playerTwo.Rating - playerOne.Rating) / 400.0));
            var playerTwoExpectedScore = 1 - playerOneExpectedScore;
           
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
        }
    }
}