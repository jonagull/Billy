import { fetchPageData } from "$lib/helpers/api";
import type { HomeFeedData } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const gamesPlayed: HomeFeedData[] = await fetchPageData("Games/homefeed");

    return {
        gamesPlayed: gamesPlayed.reverse().slice(0, 6),
    };
}) satisfies PageLoad;
