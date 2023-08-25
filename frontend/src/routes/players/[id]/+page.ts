import { fetchPageData } from "$lib/helpers/api";
import type { PlayerProfile } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async ({ params }) => {
    const data: PlayerProfile = await fetchPageData("Players/" + params.id);

    return {
        pageData: data,
    };
}) satisfies PageLoad;
