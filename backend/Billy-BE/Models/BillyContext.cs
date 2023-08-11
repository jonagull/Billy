using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class BillyContext : DbContext
{
    public BillyContext(DbContextOptions<BillyContext> options) : base(options)
    {
    }
    
    public DbSet<GamePlayed> GamesPlayed { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
}