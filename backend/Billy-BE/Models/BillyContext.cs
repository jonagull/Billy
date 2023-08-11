using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class GamePlayedContext : DbContext
{
    public DbSet<GamePlayed> GamesPlayed { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
}