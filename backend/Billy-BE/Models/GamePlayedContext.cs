using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class GamePlayedContext : DbContext
{
    protected readonly IConfiguration Configuration;

    // public GamePlayedContext (DbContextOptions<GamePlayedContext > options)
    //     : base(options)
    // {
    // }
    public GamePlayedContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<GamePlayed> GamesPlayed { get; set; } = null!;
}