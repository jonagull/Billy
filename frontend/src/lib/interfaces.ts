export interface Player {
    id: number;
    name: string;
    rating: number;
    gamesPlayed: number;
    dateCreated: number;
    wins: number;
    losses: number;
    winrate: number;
}

export interface GamePlayed {
    playerOneId: number;
    playerTwoId: number;
    winnerId: number;
}
