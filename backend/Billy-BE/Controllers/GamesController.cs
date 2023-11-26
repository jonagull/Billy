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
        
        [HttpGet("homefeed")]
        public IActionResult GetHomeFeed()
        {
            try
            {
                var gamesPlayed = _billyContext.GamesPlayed
                    .Include(game => game.PlayerOne)
                    .Include(game => game.PlayerTwo)
                    .Include(game => game.Winner)
                    .OrderByDescending(game => game.Id) // Order by Id in descending order
                    .ToList().Take(6);

                var dto = new List<object>();

                foreach (var game in gamesPlayed)
                {

                    game.PlayerOne.Rating = game.PlayerOneElo;
                    game.PlayerTwo.Rating = game.PlayerTwoElo;
                    
                    var eloChange = CalculateEloChange(game.PlayerOne, game.PlayerTwo, game.Winner.Id);
                    
                    game.TimeOfPlay = game.TimeOfPlay.AddHours(1);
                    
                    dto.Add(new
                    {
                        game,
                        eloChange
                    });
                }

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("revert/{gameId:int}")]
        public Object RevertLastGame(int gameId)
        {
            var gameToRevert = _billyContext.GamesPlayed
                .Include(game => game.PlayerOne)
                .Include(game => game.PlayerTwo)
                .Include(game => game.Winner)
                .FirstOrDefault(game => game.Id == gameId);
            
            if (gameToRevert == null)
            {
                return StatusCode(400, "Game not found");
            }

            var playerOne = _billyContext.Players.Find(gameToRevert.PlayerOne.Id);
            var playerTwo = _billyContext.Players.Find(gameToRevert.PlayerTwo.Id);
            var winner = _billyContext.Players.Find(gameToRevert.Winner.Id);

            if (playerOne == null || playerTwo == null || winner == null)
            {
                return StatusCode(400, "Player not found");
            }
            
            // TODO: Add validation on whether the players have played any games after this one

            if (playerOne.Id == winner.Id)
            {
                playerOne.Wins--;
                playerTwo.Losses--;
            }
            else
            {
                playerOne.Losses--;
                playerTwo.Wins--;
            }

            playerOne.GamesPlayed--;
            playerTwo.GamesPlayed--;

            playerOne.Rating = gameToRevert.PlayerOneElo;
            playerTwo.Rating = gameToRevert.PlayerTwoElo;
            
            _billyContext.GamesPlayed.Remove(gameToRevert);
            _billyContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllGamesPlayed(int page = 1, int pageSize = 10)
        {
            try
            {
                var totalGames = _billyContext.GamesPlayed.Count();

                var gamesPlayed = _billyContext.GamesPlayed
                    .Include(game => game.PlayerOne)
                    .Include(game => game.PlayerTwo)
                    .Include(game => game.Winner)
                    .OrderByDescending(game => game.Id) // Order by Id in descending order
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var gamesWithNames = gamesPlayed
                    .Select(
                        game =>
                            new
                            {
                                game.Id,
                                PlayerOneName = game.PlayerOne.Name,
                                PlayerTwoName = game.PlayerTwo.Name,
                                WinnerName = game.Winner.Name,
                                TimeOfPlay = game.TimeOfPlay.AddHours(1)
                            }
                    )
                    .ToList();

                var response = new
                {
                    TotalGames = totalGames,
                    PageSize = pageSize,
                    CurrentPage = page,
                    Games = gamesWithNames
                };

                return Ok(response);
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
                    gameDto.WinnerId == gameDto.PlayerOneId ? playerOne : playerTwo
                );

                // Generate a game fact based on the game outcome
                var gameFact = GenerateGameFact(playerOne, playerTwo, gameDto.WinnerId);

                UpdatePlayerMetrics(playerOne, playerTwo, gameDto.WinnerId);
                var eloChange = CalculateEloChange(playerOne, playerTwo, gameDto.WinnerId);

                if (eloChange == null)
                {
                    return StatusCode(400, "Couldn't calculate Elo ");
                }

                playerOne.Rating = eloChange.playerOneNewElo;
                playerTwo.Rating = eloChange.playerTwoNewElo;
                
                // Save changes to the database
                await _billyContext.SaveChangesAsync();

                _billyContext.Add(game);
                await _billyContext.SaveChangesAsync();
                
                var gameId = game.Id;

                var playerOneRatingDiff = playerOne.Rating - game.PlayerOneElo;
                var playerTwoRatingDiff = playerTwo.Rating - game.PlayerTwoElo;

                // Create a response object containing the updated player objects and rating differences
                var response = new
                {
                    gameId,
                    PlayerOne = new
                    {
                        NewRating = playerOne.Rating,
                        RatingDiff = playerOneRatingDiff
                    },
                    PlayerTwo = new
                    {
                        NewRating = playerTwo.Rating,
                        RatingDiff = playerTwoRatingDiff,
                    },

                    GameFact = gameFact
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

            playerOne.Winrate = (int)(
                playerOne.Losses > 0 || playerOne.Wins > 0
                    ? (playerOne.Wins * 100.0 / playerOne.GamesPlayed)
                    : 0
            );
            playerTwo.Winrate = (int)(
                playerTwo.Losses > 0 || playerTwo.Wins > 0
                    ? (playerTwo.Wins * 100.0 / playerTwo.GamesPlayed)
                    : 0
            );

            // Update the players' win streaks
            playerOne.CurrentWinStreak =
                winnerId == playerOne.Id ? playerOne.CurrentWinStreak + 1 : 0;
            playerTwo.CurrentWinStreak =
                winnerId == playerTwo.Id ? playerTwo.CurrentWinStreak + 1 : 0;

            playerOne.LongestWinStreak = Math.Max(
                playerOne.LongestWinStreak,
                playerOne.CurrentWinStreak
            );
            playerTwo.LongestWinStreak = Math.Max(
                playerTwo.LongestWinStreak,
                playerTwo.CurrentWinStreak
            );
        }
        
        private static EloChange? CalculateEloChange(Player? playerOne, Player? playerTwo, int winnerId)
        {
            const int K = 32; // Elo rating adjustment constant

            if (playerTwo == null || playerOne == null)
                return null;

            var playerOneExpectedScore = 1 / (1 + Math.Pow(10, (playerTwo.Rating - playerOne.Rating) / 400.0));
            var playerTwoExpectedScore = 1 - playerOneExpectedScore;

            // Calculate the new Elo ratings based on the winner
            var playerOneNewElo = playerOne.Rating + (int)(K * (winnerId == playerOne.Id ? 1 - playerOneExpectedScore : 0 - playerOneExpectedScore));
            var playerTwoNewElo = playerTwo.Rating + (int)(K * (winnerId == playerTwo.Id ? 1 - playerTwoExpectedScore : 0 - playerTwoExpectedScore));

            return new EloChange { playerOneNewElo = playerOneNewElo, playerTwoNewElo = playerTwoNewElo };
        }

        private static string GenerateGameFact(Player playerOne, Player playerTwo, int winnerId)
        {
            var playerOneLostWinStreak = DoesPlayerLoseWinStreak(playerOne, winnerId);
            var playerTwoLostWinStreak = DoesPlayerLoseWinStreak(playerTwo, winnerId);

            var playerOneWinStreak = playerOne.CurrentWinStreak;
            var playerTwoWinStreak = playerTwo.CurrentWinStreak;

            var gameFact = winnerId switch
            {
                _ when playerOneLostWinStreak
                    => $"{playerOne.Name} lost their {playerOneWinStreak} game win streak.",
                _ when playerTwoLostWinStreak
                    => $"{playerTwo.Name} lost their {playerTwoWinStreak} game win streak.",
                _ when winnerId == playerOne.Id && playerOneWinStreak >= 3
                    => $"{playerOne.Name} won and has a {playerOneWinStreak + 1} game win streak.", // This method runs before the win streak is updated, so we need to add 1 to the win streak
                _ when winnerId == playerTwo.Id && playerTwoWinStreak >= 3
                    => $"{playerTwo.Name} won and has a  {playerTwoWinStreak + 1} game win streak.", // This method runs before the win streak is updated, so we need to add 1 to the win streak
                _ => $"{(winnerId == playerOne.Id ? playerOne.Name : playerTwo.Name)} won the game."
            };

            return gameFact;
        }

        private static bool DoesPlayerLoseWinStreak(Player player, int winnerId)
        {
            // Player loses win streak if they had a streak of at least 3 and didn't win the game
            return player.CurrentWinStreak >= 3 && winnerId != player.Id;
        }
    }
}

class EloChange
{
    public int playerOneNewElo { get; set; }
    public int playerTwoNewElo { get; set; }
}