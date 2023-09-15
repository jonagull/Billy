<script lang="ts">
    import crown from "$lib/assets/crown.png";
    import poolSticks from "$lib/assets/poolbattle.png";
    import { baseUrl } from "$lib/constants";
    import type { PageData } from "./$types";

    export let data: PageData;
    import type { Player } from "$lib/interfaces";
    import { Alert, Button } from "stwui";
    import { onMount } from "svelte";
    import { fade, fly } from "svelte/transition";

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
    let gameFact: string;
    let dialog: HTMLDialogElement;
    let gameResult: string | undefined;

    onMount(() => {
        availablePlayers = data.players;
    });

    function handlePlayerOneSelect(event: any) {
        const playerId = +event.target.value;
        selectedPlayerOneId = playerId;

        selectedPlayerOne = data.players.find(
            (player: { id: number | undefined }) =>
                player.id === selectedPlayerOneId
        );

        updateAvailablePlayers();
    }

    function handlePlayerTwoSelect(event: any) {
        const playerId = +event.target.value;
        selectedPlayerTwoId = playerId;

        selectedPlayerTwo = data.players.find(
            (player: { id: number | undefined }) =>
                player.id === selectedPlayerTwoId
        );

        updateAvailablePlayers();
    }

    function handleWinnerSelect(event: any) {
        const playerId = +event.target.value;
        winnerId = playerId;

        selectedPlayerTwo = data.players.find(
            (player: { id: number | undefined }) =>
                player.id === selectedPlayerTwoId
        );
        updateAvailablePlayers();
    }

    function updateAvailablePlayers() {
        availablePlayers = data.players.filter(
            (player: any) => player.id !== selectedPlayerOneId
        );
    }

    function updateGameResultString() {
        if (winnerId === selectedPlayerOne?.id) {
            gameResult = `${selectedPlayerOne?.name} beat ${selectedPlayerTwo?.name}!`;
        } else
            gameResult = `${selectedPlayerTwo?.name} beat ${selectedPlayerOne?.name}!`;
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
                gameFact = data.gameFact;

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
                >{gameFact}</Alert.Description
            >
        </Alert>
    </div>
{/if}

<dialog
    class="dialog relative w-full max-w-md max-h-full rounded"
    bind:this={dialog}
    in:fly={{ y: 200, duration: 2000 }}
    out:fade
>
    <div class="flex items-start justify-between p-4 border-b rounded-t">
        <h3 class="text-xl font-semibold text-gray-700">Confirm Game</h3>
    </div>

    <div class="p-6 space-y-6">
        <div class="flex items-center space-x-1">
            <img src={crown} alt="Crown" class="w-6 h-6" />
            <p class="mb-auto text-base font-semibold text-gray-700">
                {gameResult}
            </p>
        </div>
        <p class="text-base font-normal text-gray-700" />
        Are you sure you want to log this game?
    </div>

    <div class="flex items-center p-4 space-x-2 border-t rounded-b">
        <Button
            type="primary"
            on:click={() => {
                submitForm();
                dialog.close();
            }}>Confirm</Button
        >
        <Button type="danger" on:click={() => dialog.close()}>Cancel</Button>
    </div>
</dialog>

<main>
    <div class="flex flex-col items-center">
        <img src={poolSticks} alt="Pool sticks in cross" width="200px" />

        <form class="w-2/4 max-w-md p-6 mx-auto bg-white rounded shadow-md">
            <div class="mb-6">
                <label
                    for="playerOneSelect"
                    class="block mb-2 text-sm font-bold text-gray-700"
                    >Player One:</label
                >
                <select
                    class="block w-full p-2 border border-gray-300 rounded form-select"
                    bind:value={selectedPlayerOneId}
                    on:change={handlePlayerOneSelect}
                >
                    <option value="">Select Player One</option>
                    {#each data.players as player}
                        <option value={player.id}>{player.name}</option>
                    {/each}
                </select>
            </div>

            <div class="mb-6">
                <label
                    for="playerTwoSelect"
                    class="block mb-2 text-sm font-bold text-gray-700"
                    >Player Two:</label
                >
                <select
                    class="block w-full p-2 border border-gray-300 rounded form-select"
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
                    class="block mb-2 text-sm font-bold text-gray-700"
                    >Winner:</label
                >
                <select
                    id="winnerSelect"
                    class="block w-full p-2 border border-gray-300 rounded form-select"
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
                <Button
                    type="primary"
                    disabled={!winnerId}
                    class="px-4 py-2 font-bold text-white bg-blue-500 rounded hover:bg-blue-700"
                    on:click={() => {
                        dialog.showModal();
                        updateGameResultString();
                    }}>Submit</Button
                >
            </div>
        </form>

        <div class="flex justify-center mt-10">
            <div class="w-1/2">
                {#if selectedPlayerOne}
                    <Button
                        on:click={() =>
                            handleWinnerSelect({
                                target: { value: selectedPlayerOneId },
                            })}
                    >
                        <div
                            class={`${
                                winnerId === selectedPlayerOne.id
                                    ? "bg-green-200"
                                    : ""
                            }  rounded shadow-md p-4 shadow-lg `}
                        >
                            <h2 class="mb-4 text-2xl font-bold">
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
                    </Button>
                {/if}
            </div>

            {#if selectedPlayerOne && selectedPlayerTwo}
                <div class="flex items-center mx-4">
                    <h1 class="text-4xl font-bold">VS</h1>
                </div>
            {/if}

            <div class="w-1/2">
                {#if selectedPlayerTwo}
                    <Button
                        on:click={() =>
                            handleWinnerSelect({
                                target: { value: selectedPlayerTwoId },
                            })}
                    >
                        <div
                            class={`${
                                winnerId === selectedPlayerTwo.id
                                    ? "bg-green-200"
                                    : ""
                            }  rounded shadow-md p-4 shadow-lg `}
                        >
                            <h2 class="mb-4 text-2xl font-bold">
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
                    </Button>
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

    .dialog {
        position: absolute;
        top: -2%;
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
