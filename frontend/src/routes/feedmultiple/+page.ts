import { baseUrl } from "$lib/constants";
import type { GameWithSnapshots } from "$lib/interfaces";
import { handleFetch } from "../../hooks";
import type { PageLoad } from "./$types";

export const load = (async ({ url }) => {
    // const page = url.searchParams.get("page");
    // const endpoint = `Games?page=${page || 1}&pageSize=10`;
    const res = await handleFetch("/games/multiple");

    // const response: GameWithSnapshots[] = await res.json();

    return {
        gameWithSnapshots: res.reverse(),
    };
}) satisfies PageLoad;
