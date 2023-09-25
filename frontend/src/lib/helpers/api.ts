import { baseUrl } from "$lib/constants";
import type { Player } from "$lib/interfaces";

export const fetchPageData = async (endpoint = "") => {
    const res = await fetch(`${baseUrl}/${endpoint}`);

    const response = await res.json();

    return response;
};

export async function fetchPlayers(
    sortBy: string,
    ascending: boolean
): Promise<Player[]> {
    try {
        const url = `/Players?sortBy=${sortBy}&ascending=${ascending}`;

        const response = await fetch(baseUrl + url);

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data: Player[] = await response.json();

        return data;
    } catch (error) {
        console.error("Error fetching players:", error);
        throw error;
    }
}
