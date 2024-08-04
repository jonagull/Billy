using System.Runtime.InteropServices.JavaScript;

namespace Billy_BE.DTOs;

public class GameWithSnapshotsDto
{
    public int GameId { get; set; }
    public DateTime TimeOfPlay { get; set; }
    public List<PlayerSnapshotDto> PlayerSnapshots { get; set; }
}