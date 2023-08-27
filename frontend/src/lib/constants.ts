import { dev } from "$app/environment";

export let baseUrl = "https://www.billypool.no/api";

if (dev) {
    baseUrl = import.meta.env.VITE_API_URL;
}
