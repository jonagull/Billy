import { baseUrl } from "$lib/constants";
import { fetchPageData } from "$lib/helpers/api";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const endpoint = "Games?page=1&pageSize=6";
    const res = await fetch(`${baseUrl}/${endpoint}`);

    const gamesPlayed = await res.json();

    return {
        gamesPlayed: gamesPlayed.games,
    };
}) satisfies PageLoad;
