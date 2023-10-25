using Billy_BE.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Billy_BE.Models;
using Microsoft.IdentityModel.Tokens;

namespace Billy_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly BillyContext _context;

        public PlayersController(BillyContext context)
        {
            _context = context;
        }

        [HttpGet("Elos")]
        public async Task<ActionResult<IEnumerable<PlayerEloProgressionDto>>> GetPlayersEloProgressions()
        {
            var players = await _context.Players.ToListAsync();
            var games = await _context.GamesPlayed.ToListAsync();
            List<PlayerEloProgressionDto> dto = new List<PlayerEloProgressionDto>();
            
            for (int i = 0; i < players.Count; i++)
            {
                var playerId = players[i].Id;
                var player = players[i];
                List<int> gamesPlayed = new List<int>();

                var playerEloProgression = new PlayerEloProgressionDto
                {
                    Player = player, Elos = gamesPlayed
                };
                
                // Add the starter Elo of player
                playerEloProgression.Elos.Add(1500);

                for (int j = 0; j < games.Count; j++)
                {
                    if (games[j].PlayerOne.Id == playerId || games[j].PlayerTwo.Id == playerId)
                    {
                        var playerElo = games[j].PlayerOne.Id == playerId ? games[j].PlayerOneElo : games[j].PlayerTwoElo;
                        playerEloProgression.Elos.Add(playerElo);
                    }
                }
                
                // Add the current elo that a player has
                playerEloProgression.Elos.Add(player.Rating);
                
                dto?.Add(playerEloProgression);
            }

            if (dto.IsNullOrEmpty())
            {
                return BadRequest("No eloprogressions found");
            }

            return Ok(dto);
        }


    // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers(string sortBy = "Id", bool ascending = true)
        {
            // Define the query
            IQueryable<Player> query = _context.Players;

            // Determine the property to sort by
            switch (sortBy.ToLower())
            {
                case "name":
                    query = ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;
                case "rating":
                    query = ascending ? query.OrderBy(p => p.Rating) : query.OrderByDescending(p => p.Rating);
                    break;
                case "gamesplayed":
                    query = ascending ? query.OrderBy(p => p.GamesPlayed) : query.OrderByDescending(p => p.GamesPlayed);
                    break;
                case "datecreated":
                    query = ascending ? query.OrderBy(p => p.DateCreated) : query.OrderByDescending(p => p.DateCreated);
                    break;
                case "wins":
                    query = ascending ? query.OrderBy(p => p.Wins) : query.OrderByDescending(p => p.Wins);
                    break;
                case "losses":
                    query = ascending ? query.OrderBy(p => p.Losses) : query.OrderByDescending(p => p.Losses);
                    break;
                case "winrate":
                    query = ascending ? query.OrderBy(p => p.Winrate) : query.OrderByDescending(p => p.Winrate);
                    break;
                case "currentwinstreak":
                    query = ascending ? query.OrderBy(p => p.CurrentWinStreak) : query.OrderByDescending(p => p.CurrentWinStreak);
                    break;
                case "longestwinstreak":
                    query = ascending ? query.OrderBy(p => p.LongestWinStreak) : query.OrderByDescending(p => p.LongestWinStreak);
                    break;
                default:
                    // If sortBy is not recognized, sort by Id (ascending) by default
                    query = query.OrderBy(p => p.Id);
                    break;
            }

            // Execute the query and return the sorted results
            var players = await query.ToListAsync();
            return players;
        }


        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerProfileDto>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            var gamesPlayed = await _context.GamesPlayed
                .Include(game => game.PlayerOne)
                .Include(game => game.PlayerTwo)
                .Include(game => game.Winner)
                .Where(game => game.PlayerOne.Id == id || game.PlayerTwo.Id == id)
                .ToListAsync();

            if (player == null)
            {
                return NotFound();
            }

            var dto = new PlayerProfileDto
            {
                Player = player,
                GamesPlayed = gamesPlayed
            };

            return Ok(dto);
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {

            if (player.Name.IsNullOrEmpty() || player.Name == " ")
            {
                ModelState.AddModelError("Name", "Player must have a name!");
                return BadRequest(ModelState);
            }


            // Check if the player with the same name already exists in the database
            if (_context.Players.Any(p => p.Name == player.Name))
            {
                ModelState.AddModelError("Name", "Player with this name already exists.");
                return BadRequest(ModelState);
            }

            // Player does not exist, add it to the database
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }


        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return (_context.Players?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}