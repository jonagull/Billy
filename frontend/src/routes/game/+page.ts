import { fetchPageData } from "$lib/helpers/api";
import type { Player } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const response: Player[] = await fetchPageData("Players");

    const players = response.map((player) => {
        return {
            label: player.name,
            value: player.rating,
            id: player.id,
            disabled: false,
        };
    });

    return {
        players: response.sort((a: any, b: any) =>
            a.name.localeCompare(b.name)
        ),
        mappedPlayers: players.sort((a: any, b: any) =>
            a.label.localeCompare(b.label)
        ),
    };
}) satisfies PageLoad;
