using Billy_BE.Models;

namespace Billy_BE.DTOs;

public class PlayerProfileDto
{
    public Player Player { get; set; }
    public List<GamePlayed> GamesPlayed { get; set; }
    public List<Opponent>? Opponents { get; set; }
}

public class Opponent
{
    public string? Name { get; set; }
    public int GamesAgainst { get; set; }
    public int? WinRatio { get; set; }
}