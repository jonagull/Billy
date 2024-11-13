import { baseUrl } from "$lib/constants";
import { handleFetch } from "../hooks";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const response = await handleFetch("/games/homefeed");

    // const response = await res.json();

    return {
        gamesPlayed: response,
    };
}) satisfies PageLoad;
