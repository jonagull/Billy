namespace Billy_BE.Models;

public class PlayerSnapshot
{
    public PlayerSnapshot() { }

    public int Id { get; set; }

    // public int PlayerId { get; set; } // Foreign key to the Player table
    public int EloPre { get; set; }
    public int EloPost { get; set; }
    public int EloChange { get; set; }
    public int Place { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; }
    public int? GamePlayedMultiplePlayersId { get; set; } // Foreign key to the GamePlayedMultiplePlayers table
    public GamePlayedMultiplePlayers? GamePlayedMultiplePlayers { get; set; } // Navigation property
}
