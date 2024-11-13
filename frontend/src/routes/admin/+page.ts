import { baseUrl } from "$lib/constants";
import type { PageLoad } from "@sveltejs/kit";
import { handleFetch } from "../../hooks";

export const load: PageLoad = async ({
    fetch,
}: {
    fetch: (input: RequestInfo, init?: RequestInit) => Promise<Response>;
}) => {
    const players = await handleFetch("/players");

    return {
        players,
    };
};
