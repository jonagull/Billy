import { baseUrl } from "$lib/constants";
import type { PageLoad } from "./$types";

export const load = (async ({ url }) => {
    const page = url.searchParams.get("page");
    const endpoint = `Games?page=${page || 1}&pageSize=10`;

    const res = await fetch(`${baseUrl}/${endpoint}`);

    const response = await res.json();

    return {
        totalGames: response.totalGames,
        pageSize: response.pageSize,
        currentPage: response.currentPage,
        gamesPlayed: response.games,
    };
}) satisfies PageLoad;
