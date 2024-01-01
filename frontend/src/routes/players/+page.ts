import { fetchPlayers } from "$lib/helpers/api";
import type { PageLoad } from "./$types";

export const load = (async ({ params, url }) => {
    const orderBy = url.searchParams.get("orderBy");
    const order = url.searchParams.get("order");

    const players = await fetchPlayers(
        orderBy || "rating",
        order === "asc" ? true : false
    );

    return {
        players: players,
    };
}) satisfies PageLoad;
