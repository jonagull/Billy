export interface Player {
    id: number;
    name: string;
    rating: number;
    gamesPlayed: number;
    dateCreated: number;
    wins: number;
    losses: number;
    winrate: number;
    currentWinStreak: number;
    longestWinStreak: number;
}

export interface GamePlayed {
    id: number;
    playerOneElo: number;
    playerTwoElo: number;
    playerOne: Player;
    playerTwo: Player;
    winner: Player;
    timeOfPlay: Date;
}

export interface PlayerProfile {
    player: Player;
    gamesPlayed: GamePlayed[];
}
