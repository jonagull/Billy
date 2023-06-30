using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class GamePlayedContext : DbContext
{
    public GamePlayedContext (DbContextOptions<GamePlayedContext > options)
        : base(options)
    {
    }

    public DbSet<GamePlayed> GamesPlayed { get; set; } = null!;
}