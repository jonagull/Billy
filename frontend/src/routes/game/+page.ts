import { fetchPageData } from "$lib/helpers/api";
import type { PageLoad } from "./$types";

export const load = (async () => {
    const response: any = await fetchPageData("Players");

    return {
        players: response.sort((a: any, b: any) =>
            a.name.localeCompare(b.name)
        ),
    };
}) satisfies PageLoad;
