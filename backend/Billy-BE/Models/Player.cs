namespace Billy_BE.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int TenantId { get; set; } // Foreign key to Tenant
        public string Name { get; set; }
        public int Rating { get; set; }
        public int GamesPlayed { get; set; }
        public DateTime DateCreated { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Winrate { get; set; }
        public int CurrentWinStreak { get; set; }
        public int LongestWinStreak { get; set; }
    }
}
