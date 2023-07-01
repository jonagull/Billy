<script lang="ts">
    import type { Player } from "$lib/images/interfaces";
    import { Input } from "stwui";
    import { Button } from "stwui";
    import { onMount } from "svelte";

    let playerName = "";
    let players: Player[] = [];

    onMount(() => {
        getPlayers();
    });

    function addPlayer() {
        const playerData = {
            name: playerName,
            rating: 1500,
            gamesPlayed: 0,
            dateCreated: new Date().toISOString(),
        };

        fetch("http://localhost:5219/api/Players", {
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
                    await getPlayers();
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

    async function getPlayers() {
        try {
            const response = await fetch("http://localhost:5219/api/Players");
            if (response.ok) {
                players = await response.json();
            } else {
                console.error("Failed to retrieve players");
            }
        } catch (error) {
            console.error("An error occurred while retrieving players", error);
        }
    }

    async function removePlayer(id: number) {
        try {
            const response = await fetch(
                `http://localhost:5219/api/Players/${id}`,
                {
                    method: "DELETE",
                }
            );

            if (response.ok) {
                getPlayers();
            } else {
                console.error("Failed to remove player");
            }
        } catch (error) {
            console.error("Error removing player:", error);
        }
    }
</script>

<main>
    <h1>Players</h1>

    <div class="flex mb-20">
        <Input name="input" placeholder="Name" bind:value={playerName} />

        <Button on:click={addPlayer} type="primary">Add</Button>
    </div>

    <h2 class="text-2xl font-bold mb-2">Players</h2>
    <table class="min-w-full divide-y divide-gray-200">
        <thead class="bg-gray-50">
            <tr>
                <th
                    scope="col"
                    class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Name
                </th>
                <th
                    scope="col"
                    class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Rating
                </th>
                <th
                    scope="col"
                    class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Games Played
                </th>
                <th
                    scope="col"
                    class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Wins
                </th>
                <th
                    scope="col"
                    class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Losses
                </th>
                <th
                    scope="col"
                    class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Winrate
                </th>
                <th
                    scope="col"
                    class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Creation Date
                </th>
                <th
                    scope="col"
                    class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Actions
                </th>
            </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
            {#each players as player (player.id)}
                <tr>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            bind:value={player.name}
                        />
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            bind:value={player.rating}
                        />
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            bind:value={player.gamesPlayed}
                        />
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            bind:value={player.wins}
                        />
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            bind:value={player.losses}
                        />
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            bind:value={player.winrate}
                        />
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            bind:value={player.dateCreated}
                        />
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <Button
                            on:click={() => removePlayer(player.id)}
                            type="danger"
                            class="text-white bg-red-500 hover:bg-red-600"
                        >
                            Remove
                        </Button>
                    </td>
                </tr>
            {/each}
        </tbody>
    </table>
</main>
