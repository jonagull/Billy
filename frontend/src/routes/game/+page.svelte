<script lang="ts">
    import { Alert } from "stwui";
    import { fade, fly } from "svelte/transition";
    import { onMount } from "svelte";
    import poolSticks from "$lib/assets/poolbattle.png";
    import type { Player } from "$lib/interfaces";
    import { baseUrl } from "$lib/constants";
    import { shortQuotes } from "$lib/data/quotes";

    let winnerId: number | undefined;
    let players: Player[] = [];
    let selectedPlayerOneId: number | undefined;
    let selectedPlayerOne: Player | undefined;
    let selectedPlayerTwoId: number | undefined;
    let selectedPlayerTwo: Player | undefined;
    let availablePlayers: Player[] = [];
    let playerOneRatingChange: number;
    let playerTwoRatingChange: number;
    let showAlert = false;

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
            const response = await fetch(baseUrl + "/Players");
            if (response.ok) {
                players = await response.json();
                players.sort((a, b) => a.name.localeCompare(b.name));
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
            playerTwoId: selectedPlayerTwoId,
            winnerId: winnerId,
        };
        try {
            const response = await fetch(baseUrl + "/Games", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Accept: "application/json",
                },
                body: JSON.stringify(game),
            });

            if (response.ok) {
                // Handle successful response
                showAlert = true;
                selectedPlayerOneId = undefined;
                selectedPlayerTwoId = undefined;
                winnerId = undefined;
                const data = await response.json();
                playerOneRatingChange = data.playerOne.ratingDiff;
                playerTwoRatingChange = data.playerTwo.ratingDiff;

                setTimeout(() => {
                    showAlert = false;
                }, 7000);
            } else {
                // Handle error response
                console.error("Failed to submit game.");
            }
        } catch (error) {
            console.error("An error occurred:", error);
        }
    }
</script>

{#if showAlert}
    <div class="alert" in:fly={{ y: 200, duration: 2000 }} out:fade>
        <Alert type="success">
            <Alert.Title slot="title">Game logged!</Alert.Title>
            <Alert.Description slot="description" class="italic"
                >{shortQuotes[Math.floor(Math.random() * shortQuotes.length)]
                    .quote}</Alert.Description
            >
        </Alert>
    </div>
{/if}

<main>
    <div class="flex flex-col items-center">
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
                        <span class="flex">
                            <p>
                                Elo: {selectedPlayerOne.rating}
                            </p>
                            {#if playerOneRatingChange}
                                <p
                                    class={playerOneRatingChange > 0
                                        ? "green-text"
                                        : "red-text"}
                                >
                                    {playerOneRatingChange > 0 ? " +" : " "}
                                    {playerOneRatingChange}
                                </p>
                            {/if}
                        </span>
                        <p>
                            Games played: {selectedPlayerOne.gamesPlayed}
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
                        <span class="flex">
                            <p>
                                Elo: {selectedPlayerTwo.rating}
                            </p>
                            {#if playerTwoRatingChange}
                                <p
                                    class={playerTwoRatingChange > 0
                                        ? "green-text"
                                        : "red-text"}
                                >
                                    {playerTwoRatingChange > 0
                                        ? "" + " +"
                                        : " "}
                                    {playerTwoRatingChange}
                                </p>
                            {/if}
                        </span>
                        <p>
                            Games played: {selectedPlayerTwo.gamesPlayed}
                        </p>
                    </div>
                {/if}
            </div>
        </div>
    </div>
</main>

<style>
    .alert {
        position: fixed;
        top: 10%;
        right: 20px;
        z-index: 1000;
        width: 400px;
    }

    .green-text {
        color: #629924;
    }

    .red-text {
        color: #cc3333;
    }
</style>
