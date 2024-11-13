import { baseUrl } from "$lib/constants";
import { fetchPlayers } from "$lib/helpers/api";
import { handleFetch } from "../../hooks";
import type { PageLoad } from "./$types";

export const load = (async ({ params, url }) => {
    const orderBy = url.searchParams.get("orderBy");
    const order = url.searchParams.get("order");
    const host = url.host;
    const subdomain = host.split(".")[0]; // Extract subdomain

    const endpoint = `/players?sortBy=${
        orderBy || "rating"
    }&ascending=${"true"}`;
    const response = await handleFetch(endpoint);

    return {
        players: response,
    };
}) satisfies PageLoad;
