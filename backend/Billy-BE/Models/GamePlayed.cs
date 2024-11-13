namespace Billy_BE.Models;

public class GamePlayed
{
    public int Id { get; private set; }
    public int TenantId { get; set; } // Foreign key to Tenant
    public int PlayerOneElo { get; private set; }
    public int PlayerTwoElo { get; private set; }
    public DateTime TimeOfPlay { get; set; }
    public Player PlayerOne { get; private set; }
    public Player PlayerTwo { get; private set; }
    public Player Winner { get; private set; }

    private GamePlayed() { }

    public GamePlayed(Player playerOne, Player playerTwo, Player winner)
    {
        if (winner != playerOne && winner != playerTwo)
        {
            throw new ArgumentException("Winner must be one of the players", nameof(winner));
        }

        TimeOfPlay = DateTime.UtcNow;
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        Winner = winner;
        PlayerOneElo = playerOne.Rating;
        PlayerTwoElo = playerTwo.Rating;
    }
}
