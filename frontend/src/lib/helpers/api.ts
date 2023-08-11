import { baseUrl } from "$lib/constants";

export const fetchPageData = async (endpoint = "") => {
    const res = await fetch(`${baseUrl}/${endpoint}`);

    const response = await res.json();

    return response;
};
