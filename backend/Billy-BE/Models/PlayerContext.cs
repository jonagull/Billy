using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class PlayerContext : DbContext
{
    private readonly IConfiguration _configuration;
    public PlayerContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_configuration.GetConnectionString("WebApiDatabase"));
    }
    public DbSet<Player> Players { get; set; } = null!;
}