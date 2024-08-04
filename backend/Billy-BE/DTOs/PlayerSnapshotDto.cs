namespace Billy_BE.DTOs;

public class PlayerSnapshotDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PlayerId { get; set; }
    public int EloChange { get; set; }
    public int EloPre { get; set; }
    public int EloPost { get; set; }
    public int Place { get; set; }
}