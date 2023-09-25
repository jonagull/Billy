import type { TableColumn } from "stwui/types";

export const columns: TableColumn[] = [
    {
        column: "name",
        label: "Name",
        placement: "left",
    },
    {
        column: "rating",
        label: "Elo",
        placement: "left",
    },
    {
        column: "gamesPlayed",
        label: "Games played",
        placement: "left",
    },
    {
        column: "wins",
        label: "Wins",
        placement: "left",
    },
    {
        column: "losses",
        label: "Losses",
        placement: "left",
    },
    {
        column: "winrate",
        label: "Winrate",
        placement: "left",
    },
    {
        column: "currentWinStreak",
        label: "Current winstreak",
        placement: "left",
    },
    {
        column: "longestWinStreak",
        label: "Longest winstreak",
        placement: "left",
    },
    {
        column: "dateCreated",
        label: "Date created",
        placement: "left",
    },
];
