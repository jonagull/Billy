import { fetchPageData } from "$lib/helpers/api";
import type { PlayerProfile } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const response: any = await fetchPageData("Players");

    return {
        players: response,
    };
}) satisfies PageLoad;
