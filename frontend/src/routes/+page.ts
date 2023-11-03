import { fetchPageData } from "$lib/helpers/api";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const gamesPlayed = await fetchPageData("Games");

    return {
        gamesPlayed: gamesPlayed.reverse().slice(0, 6),
    };
}) satisfies PageLoad;
