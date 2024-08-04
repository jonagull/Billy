using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class BillyContext : DbContext
{
    public BillyContext(DbContextOptions<BillyContext> options) : base(options)
    {
    }
    
    public DbSet<GamePlayed> GamesPlayed { get; set; } = null!;
    public DbSet<GamePlayedMultiplePlayers> GamePlayedMultiplePlayers { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<PlayerSnapshot> PlayerSnapshots { get; set; } = null!;
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     // modelBuilder.Entity<GamePlayedMultiplePlayers>()
    //     //     .HasMany(e => e.PlayerSnapshots)
    //     //     .WithOne(e => e.GamePlayedMultiplePlayers)
    //     //     .HasForeignKey(e => e.GamePlayedMultiplePlayersId)
    //     //     .IsRequired();
    // }
}