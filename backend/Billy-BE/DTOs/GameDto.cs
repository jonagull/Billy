using Billy_BE.DTOs;

public class GameDto
{
    public int GameId { get; set; }
    public DateTime TimeOfPlay { get; set; }
    public List<PlayerSnapshotDto>? PlayerSnapshots { get; set; }
}
