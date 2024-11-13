using System;
using System.Collections.Generic;

namespace Billy_BE.Models
{
    public class GamePlayedMultiplePlayers
    {
        public int Id { get; set; }
        public int TenantId { get; set; } // Foreign key to Tenant
        public DateTime TimeOfPlay { get; set; }

        // Navigation property representing the player snapshots associated with this game
        public ICollection<PlayerSnapshot> PlayerSnapshots { get; set; }

        public GamePlayedMultiplePlayers() { }

        public GamePlayedMultiplePlayers(List<PlayerSnapshot> playerSnapshots)
        {
            TimeOfPlay = DateTime.UtcNow;
            PlayerSnapshots = playerSnapshots;
        }
    }
}
