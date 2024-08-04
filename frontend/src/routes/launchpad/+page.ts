import { baseUrl } from "$lib/constants";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const endpoint = "games/multiple";
    const res = await fetch(`${baseUrl}/${endpoint}`);

    const response = await res.json();

    return {
        games: response,
    };
}) satisfies PageLoad;
