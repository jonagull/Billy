export interface Player {
    id: number;
    name: string;
    rating: number;
    gamesPlayed: number;
}

export interface GamePlayed {
    playerOneId: number;
    playerOneElo: number;
    playerTwoId: number;
    playerTwoElo: number;
    winnerId: number;
    timeOfPlay: Date;
}
