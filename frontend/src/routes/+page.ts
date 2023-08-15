// since there's no dynamic data here, we can prerender
// it so that it gets served as a static asset in production
export const prerender = true;
import { fetchPageData } from "$lib/helpers/api";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const gamesPlayed = await fetchPageData("Games");

    return {
        gamesPlayed: gamesPlayed.reverse().slice(0, 5),
    };
}) satisfies PageLoad;
