<script lang="ts">
    import type { Player } from "$lib/images/interfaces";
    import { onMount } from "svelte";
    import poolSticks from "$lib/images/assets/poolbattle.png";

    let winnerId: number | undefined;
    let timeOfPlay = new Date().toISOString();
    let players: Player[] = [];
    let selectedPlayerOneId: number | undefined;
    let selectedPlayerOne: Player | undefined;
    let selectedPlayerTwoId: number | undefined;
    let selectedPlayerTwo: Player | undefined;
    let availablePlayers: Player[] = [];

    onMount(() => {
        fetchPlayers();
        availablePlayers = players;
    });

    function handlePlayerOneSelect(event: any) {
        const playerId = +event.target.value;
        selectedPlayerOneId = playerId;

        selectedPlayerOne = players.find(
            (player) => player.id === selectedPlayerOneId
        );

        updateAvailablePlayers();
    }

    function handlePlayerTwoSelect(event: any) {
        const playerId = +event.target.value;
        selectedPlayerTwoId = playerId;

        selectedPlayerTwo = players.find(
            (player) => player.id === selectedPlayerTwoId
        );
        updateAvailablePlayers();
    }

    function handleWinnerSelect(event: any) {
        const playerId = +event.target.value;
        winnerId = playerId;

        selectedPlayerTwo = players.find(
            (player) => player.id === selectedPlayerTwoId
        );
        updateAvailablePlayers();
    }

    function updateAvailablePlayers() {
        availablePlayers = players.filter(
            (player) => player.id !== selectedPlayerOneId
        );
    }

    async function fetchPlayers() {
        try {
            const response = await fetch("http://localhost:5219/api/Players");
            if (response.ok) {
                players = await response.json();
            } else {
                console.error("Failed to fetch players.");
            }
        } catch (error) {
            console.error("An error occurred:", error);
        }
    }

    async function submitForm() {
        if (!winnerId) {
            return;
        }

        const game = {
            playerOneId: selectedPlayerOneId,
            playerOneElo: selectedPlayerOne?.rating,
            playerTwoId: selectedPlayerTwoId,
            playerTwoElo: selectedPlayerTwo?.rating,
            winnerId: winnerId,
            timeOfPlay: timeOfPlay,
        };
        try {
            const response = await fetch("http://localhost:5219/api/Games", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Accept: "application/json",
                },
                body: JSON.stringify(game),
            });

            if (response.ok) {
                // Handle successful response
                console.log("Game submitted successfully!");
            } else {
                // Handle error response
                console.error("Failed to submit game.");
            }
        } catch (error) {
            console.error("An error occurred:", error);
        }
    }
</script>

<main>
    <div class="flex flex-col items-center">
        <h1 class="mb-10">Submit Game</h1>

        <img src={poolSticks} alt="Pool sticks in cross" width="200px" />

        <form
            class="max-w-md w-2/4 mx-auto p-6 bg-white rounded shadow-md"
            on:submit|preventDefault={submitForm}
        >
            <div class="mb-6">
                <label
                    for="playerOneSelect"
                    class="block text-gray-700 text-sm font-bold mb-2"
                    >Player One:</label
                >
                <select
                    id="playerOneSelect"
                    class="form-select block w-full p-2 border border-gray-300 rounded"
                    bind:value={selectedPlayerOneId}
                    on:change={handlePlayerOneSelect}
                >
                    <option value="">Select Player One</option>
                    {#each players as player}
                        <option value={player.id}>{player.name}</option>
                    {/each}
                </select>
            </div>

            <div class="mb-6">
                <label
                    for="playerTwoSelect"
                    class="block text-gray-700 text-sm font-bold mb-2"
                    >Player Two:</label
                >
                <select
                    id="playerTwoSelect"
                    class="form-select block w-full p-2 border border-gray-300 rounded"
                    bind:value={selectedPlayerTwoId}
                    on:change={handlePlayerTwoSelect}
                >
                    <option value="">Select Player Two</option>
                    {#each availablePlayers as player}
                        <option value={player.id}>{player.name}</option>
                    {/each}
                </select>
            </div>

            <div class="mb-6">
                <label
                    for="winnerSelect"
                    class="block text-gray-700 text-sm font-bold mb-2"
                    >Winner:</label
                >
                <select
                    id="winnerSelect"
                    class="form-select block w-full p-2 border border-gray-300 rounded"
                    bind:value={winnerId}
                    on:change={handleWinnerSelect}
                >
                    <option value="">Select Winner</option>
                    {#each [selectedPlayerOne, selectedPlayerTwo] as player}
                        <option value={player?.id}>{player?.name}</option>
                    {/each}
                </select>
            </div>

            <div class="text-center">
                <button
                    type="submit"
                    class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                    >Submit</button
                >
            </div>
        </form>

        <div class="flex justify-center mt-10">
            <div class="w-1/2">
                {#if selectedPlayerOne}
                    <div
                        class={`${
                            winnerId === selectedPlayerOne.id
                                ? "bg-green-200"
                                : ""
                        }  rounded shadow-md p-4 shadow-lg `}
                    >
                        <h2 class="text-2xl font-bold mb-4">
                            {selectedPlayerOne.name}
                        </h2>
                        <p>
                            Elo: {selectedPlayerOne.rating} | Games played: {selectedPlayerOne.gamesPlayed}
                        </p>
                    </div>
                {/if}
            </div>

            {#if selectedPlayerOne && selectedPlayerTwo}
                <div class="flex items-center mx-4">
                    <h1 class="text-4xl font-bold">VS</h1>
                </div>
            {/if}

            <div class="w-1/2">
                {#if selectedPlayerTwo}
                    <div
                        class={`${
                            winnerId === selectedPlayerTwo.id
                                ? "bg-green-200"
                                : ""
                        }  rounded shadow-md p-4 shadow-lg `}
                    >
                        <h2 class="text-2xl font-bold mb-4">
                            {selectedPlayerTwo.name}
                        </h2>
                        <p>
                            Elo: {selectedPlayerTwo.rating} | Games played: {selectedPlayerTwo.gamesPlayed}
                        </p>
                    </div>
                {/if}
            </div>
        </div>
    </div>
</main>
