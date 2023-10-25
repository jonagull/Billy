using Billy_BE.Models;

namespace Billy_BE.DTOs;

public class PlayerEloProgressionDto
{
    public Player Player { get; set; }
    public List<int> Elos { get; set; }
}
