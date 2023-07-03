using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class GamePlayedContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public GamePlayedContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<GamePlayed> GamesPlayed { get; set; } = null!;
}