import { baseUrl } from "$lib/constants";
import type { GameWithSnapshots } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async ({ url }) => {
    // const page = url.searchParams.get("page");
    // const endpoint = `Games?page=${page || 1}&pageSize=10`;

    // const res = await fetch(`${baseUrl}/${endpoint}`);
    const endpoint = "games/multiple";
    const res = await fetch(`${baseUrl}/${endpoint}`);

    const response: GameWithSnapshots[] = await res.json();

    return {
        gameWithSnapshots: response.reverse(),
    };
}) satisfies PageLoad;
