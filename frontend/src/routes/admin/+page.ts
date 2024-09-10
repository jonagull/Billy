import { baseUrl } from "$lib/constants";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const response = await fetch(`${baseUrl}/players`);
    const players = await response.json();

    return {
        players: players,
    };
}) satisfies PageLoad;
