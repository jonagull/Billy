using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class PlayerContext : DbContext
{
    protected readonly IConfiguration Configuration;
    // public PlayerContext(DbContextOptions<PlayerContext> options)
    //     : base(options)
    // {
    // }
    public PlayerContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
    }
    public DbSet<Player> Players { get; set; } = null!;
}