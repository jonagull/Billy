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
    opponents: Opponent[];
}

export interface PlayerEloProgression {
    player: Player;
    elos: number[];
}

export interface Opponent {
    name?: string;
    gamesAgainst?: number;
    winRatio?: number;
}

export interface HomeFeed {
    game: GamePlayed;
    eloChange: EloChange;
}

export interface EloChange {
    playerOneNewElo: number;
    playerTwoNewElo: number;
}

export interface GameWithSnapshots {
    gameId: number;
    timeOfPlay: Date;
    playerSnapshots: PlayerSnapShot[];
}

export interface PlayerSnapShot {
    id: number;
    name: string;
    playerId: number;
    eloChange: number;
    eloPre: number;
    eloPost: number;
    place: number;
}

export type PlayerSnapshot = {
    id: number;
    name: string;
    playerId: number;
    eloChange: number;
    eloPre: number;
    eloPost: number;
    place: number;
};
