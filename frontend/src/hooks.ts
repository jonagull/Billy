import { baseUrl } from "$lib/constants";

export const handleFetch = async (endpoint: string) => {
    if (!endpoint) {
        return;
    }

    const tenantId = window.localStorage.getItem("tenantId");

    if (!tenantId) {
        console.error("Tenant ID not found in local storage");
        return;
    }

    const response = await fetch(baseUrl + endpoint, {
        method: "GET", // or 'POST', 'PUT', etc.
        headers: {
            "Content-Type": "application/json", // Example of specifying the content type
            // Authorization: "Bearer your-auth-token", // Example of adding an Authorization token
            "X-Tenant-ID": tenantId, // Custom header for tenant ID
        },
    });

    const data = await response.json();

    return data;
};
