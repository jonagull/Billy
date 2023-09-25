<script lang="ts">
    import { baseUrl } from "$lib/constants";

    import { goto } from "$app/navigation";
    import type { PageData } from "./$types";
    import TheTable from "$lib/components/TheTable.svelte";

    export let data: PageData;

    let playerName = "";

    function addPlayer() {
        const playerData = {
            name: playerName,
            rating: 1500,
            gamesPlayed: 0,
            dateCreated: new Date().toISOString(),
        };

        fetch(baseUrl + "/Players", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(playerData),
        })
            .then(async (response) => {
                if (response.ok) {
                    console.log("Player added successfully");
                    playerName = ""; // Reset the player name
                } else {
                    console.error("Failed to add player");
                }
            })
            .catch((error) => {
                console.error(
                    "An error occurred while adding the player",
                    error
                );
            });
    }

    const goToPlayer = (id: number) => {
        goto(`players/${id}`, { replaceState: true });
    };
</script>

<TheTable players={data.players} />
