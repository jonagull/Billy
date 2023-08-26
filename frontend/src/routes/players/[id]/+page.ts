import { fetchPageData } from "$lib/helpers/api";
import type { PlayerProfile } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async ({ params }) => {
    const response: PlayerProfile = await fetchPageData("Players/" + params.id);

    return {
        pageData: response,
        gamesPlayed: response.gamesPlayed.reverse(),
        player: response.player,
    };
}) satisfies PageLoad;
