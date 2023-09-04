import { fetchPageData } from "$lib/helpers/api";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const response: any[] = await fetchPageData("Players");

    return {
        players: response.sort((a: any, b: any) => b.rating - a.rating),
    };
}) satisfies PageLoad;
