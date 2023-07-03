namespace Billy_BE.Models;

public class GamePlayed
{
    public int Id { get; set; }
    public int PlayerOneId { get; set; }
    public int PlayerOneElo { get; set; }
    public int PlayerTwoId { get; set; }
    public int PlayerTwoElo { get; set; }
    public int WinnerId { get; set; }
    public DateTime TimeOfPlay { get; set; }
}