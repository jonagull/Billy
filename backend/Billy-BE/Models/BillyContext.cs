using Microsoft.EntityFrameworkCore;

namespace Billy_BE.Models;

public class BillyContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BillyContext(
        DbContextOptions<BillyContext> options,
        IHttpContextAccessor httpContextAccessor
    )
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<GamePlayed> GamesPlayed { get; set; } = null!;
    public DbSet<GamePlayedMultiplePlayers> GamePlayedMultiplePlayers { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<PlayerSnapshot> PlayerSnapshots { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Global filter for TenantId
        modelBuilder.Entity<Player>().HasQueryFilter(p => p.TenantId == GetTenantId());
        modelBuilder
            .Entity<GamePlayedMultiplePlayers>()
            .HasQueryFilter(g => g.TenantId == GetTenantId());
        // Add filters for other entities if needed

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        SetTenantId();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTenantId();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetTenantId()
    {
        if (
            _httpContextAccessor.HttpContext != null
            && _httpContextAccessor.HttpContext.Items.ContainsKey("TenantId")
        )
        {
            var tenantId = (int)_httpContextAccessor.HttpContext.Items["TenantId"];

            // Find all entities that implement the TenantId property and are newly added
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                    (e.Entity is Player || e.Entity is GamePlayedMultiplePlayers)
                    && e.State == EntityState.Added
                )
                .ToList();

            foreach (var entry in entries)
            {
                if (entry.Entity is Player player)
                {
                    player.TenantId = tenantId;
                }
                else if (entry.Entity is GamePlayedMultiplePlayers game)
                {
                    game.TenantId = tenantId;
                }
            }
        }
    }

    private int GetTenantId()
    {
        if (
            _httpContextAccessor.HttpContext != null
            && _httpContextAccessor.HttpContext.Items.ContainsKey("TenantId")
        )
        {
            return (int)_httpContextAccessor.HttpContext.Items["TenantId"];
        }

        // Return a default TenantId or handle the case where TenantId is missing
        throw new Exception("TenantId not found in HttpContext.");
    }
}
