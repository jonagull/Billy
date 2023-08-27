using Billy_BE.Models;

namespace Billy_BE.DTOs;

public class PlayerProfileDto
{
    public Player Player { get; set; }
    public List<GamePlayed> GamesPlayed { get; set; }
    public Player PlayerNemesis { get; set; }
}