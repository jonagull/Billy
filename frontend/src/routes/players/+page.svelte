<script lang="ts">
    import { baseUrl } from "$lib/constants";
    import { Input } from "stwui";
    import { Button } from "stwui";
    import { onMount } from "svelte";
    import { Modal, Portal } from "stwui";
    import type { Player } from "$lib/interfaces";
    import { goto } from "$app/navigation";

    let playerName = "";
    let players: Player[] = [];
    let open = false;

    onMount(() => {
        getPlayers();
    });

    function formatDateTime(dateTimeString: string | number | Date) {
        const dateTime = new Date(dateTimeString);
        const options = { day: "numeric", month: "numeric", year: "numeric" };
        return dateTime.toLocaleDateString(
            "no",
            options as Intl.DateTimeFormatOptions
        );
    }

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
                    await getPlayers();
                    closeModal();
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
            const response = await fetch(baseUrl + "/Players");
            if (response.ok) {
                players = await response.json();
                players.sort((a, b) => b.rating - a.rating);
            } else {
                console.error("Failed to retrieve players");
            }
        } catch (error) {
            console.error("An error occurred while retrieving players", error);
        }
    }

    function openModal() {
        open = true;
    }

    function closeModal() {
        open = false;
    }

    const goToPlayer = (id: number) => {
        goto(`players/${id}`, { replaceState: true });
    };
</script>

<main>
    <Button class="mb-5" type="primary" on:click={openModal}>Add player</Button>

    <Portal>
        {#if open}
            <Modal handleClose={closeModal}>
                <Modal.Content slot="content">
                    <Modal.Content.Header slot="header"
                        >Add player</Modal.Content.Header
                    >
                    <Modal.Content.Body slot="body">
                        <Input
                            name="input"
                            placeholder="Name"
                            bind:value={playerName}
                        />

                        <Button class="mt-3" on:click={addPlayer} type="primary"
                            >Add</Button
                        >
                    </Modal.Content.Body>
                </Modal.Content>
            </Modal>
        {/if}
    </Portal>

    <!-- Might have to implement backend pagination for this to work -->
    <!-- <TheTable data={players} /> -->

    <h2 class="text-2xl font-bold mb-2">Players</h2>
    <table class="table-auto min-w-full divide-y divide-gray-200">
        <thead class="bg-gray-50">
            <tr>
                <th
                    scope="col"
                    class="w-40 px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Name
                </th>
                <th
                    scope="col"
                    class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Rating
                </th>
                <th
                    scope="col"
                    class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Games Played
                </th>
                <th
                    scope="col"
                    class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Wins
                </th>
                <th
                    scope="col"
                    class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Losses
                </th>
                <th
                    scope="col"
                    class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Winrate
                </th>
                <th
                scope="col"
                class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                Current Winstreak
                </th>
                <th
                scope="col"
                class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                Longest Winstreak
                 </th>
                <th
                    scope="col"
                    class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                    Created
                </th>
            </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
            {#each players as player (player.id)}
                <tr on:click={() => goToPlayer(player.id)}>
                    <td
                        on:click={() => goToPlayer(player.id)}
                        class="px-4 py-2 tespace-nowrap"
                    >
                        <!-- <a href={`/players/${player.id}`}>{player.name}</a> -->
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={player.name}
                        />
                    </td>
                    <td
                        on:click={() => goToPlayer(player.id)}
                        class="px-4 py-2 tespace-nowrap"
                    >
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={player.rating}
                        />
                    </td>
                    <td
                        on:click={() => goToPlayer(player.id)}
                        class="px-4 py-2 tespace-nowrap"
                    >
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={player.gamesPlayed}
                        />
                    </td>
                    <td
                        on:click={() => goToPlayer(player.id)}
                        class="px-4 py-2 tespace-nowrap"
                    >
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={player.wins}
                        />
                    </td>
                    <td
                        on:click={() => goToPlayer(player.id)}
                        class="px-4 py-2 tespace-nowrap"
                    >
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={player.losses}
                        />
                    </td>
                    <td
                        on:click={() => goToPlayer(player.id)}
                        class="px-4 py-2 tespace-nowrap"
                    >
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={player.winrate + "%"}
                        />
                    </td>
                    <td
                        on:click={() => goToPlayer(player.id)}
                        class="px-4 py-2 tespace-nowrap"
                    >
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={player.currentWinStreak}
                        />
                    </td>
                    <td class="px-4 py-2 tespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={player.longestWinStreak}
                        />
                    </td>
                    <td class="px-4 py-2 tespace-nowrap">
                        <input
                            readonly
                            name="input"
                            class="border-none"
                            value={formatDateTime(player.dateCreated)}
                        />
                    </td>
                </tr>
            {/each}
        </tbody>
    </table>
</main>

<style>
    input {
        width: 100px;
    }

    tr:hover input {
        background-color: lightgray; /* Change to your preferred color */
        cursor: pointer;
    }

    /* input:hover {
        cursor: pointer;
    } */

    tr:hover {
        background-color: lightgray; /* Change to your preferred color */
        cursor: pointer;
    }
</style>
