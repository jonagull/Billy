import { baseUrl } from "$lib/constants";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const endpoint = "games/homefeed";
    const res = await fetch(`${baseUrl}/${endpoint}`);

    const response = await res.json();

    return {
        gamesPlayed: response,
    };
}) satisfies PageLoad;
