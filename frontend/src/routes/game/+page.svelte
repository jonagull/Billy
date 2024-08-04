<script lang="ts">
    import billyLogo from "$lib/assets//milkybilly.png";
    import fingerPointing from "$lib/assets/fingerpoint.png";
    import type { PageData } from "./$types";
    import type { Player } from "$lib/interfaces";
    import { Alert, Button } from "stwui";
    import { onMount } from "svelte";
    import { fade, fly } from "svelte/transition";
    import TheComboBox from "$lib/components/TheComboBox.svelte";
    import { baseUrl } from "$lib/constants";
    import TheProgressBar from "$lib/components/TheProgressBar.svelte";
    import TheRevertButton from "$lib/components/TheRevertButton.svelte";

    export let data: PageData;

    let winnerId: number | undefined;
    let selectedPlayerOneId: number | undefined;
    let selectedPlayerOne: Player | undefined;
    let selectedPlayerTwoId: number | undefined;
    let selectedPlayerTwo: Player | undefined;
    let availablePlayers: any[] = [];
    let playerOneRatingChange: number;
    let playerTwoRatingChange: number;
    let showAlert = false;
    let gameFact: string;
    let gameResult: string | undefined;
    let showHand: boolean = true;
    let showRevert: boolean = false;
    let gameId: number;
    let isReverting: boolean = false;
    let logError: string = "Error logging game!";
    let showError: boolean = false;

    onMount(() => {
        availablePlayers = data.mappedPlayers;
    });

    $: {
        if (selectedPlayerOneId) {
            selectedPlayerOne = data.players.find(
                (player: { id: number | undefined }) =>
                    player.id === selectedPlayerOneId
            );

            updateAvailablePlayers();
        }

        if (selectedPlayerTwoId) {
            selectedPlayerTwo = data.players.find(
                (player: { id: number | undefined }) =>
                    player.id === selectedPlayerTwoId
            );
        }
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

    const updateAvailablePlayers = () => {
        availablePlayers = data.mappedPlayers.filter(
            (player: any) => player.id !== selectedPlayerOneId
        );
    };

    function updateGameResultString() {
        if (winnerId === selectedPlayerOne?.id) {
            gameResult = `${selectedPlayerOne?.name} beat ${selectedPlayerTwo?.name}!`;
        } else
            gameResult = `${selectedPlayerTwo?.name} beat ${selectedPlayerOne?.name}!`;
    }

    async function logGame() {
        if (
            (selectedPlayerOne && !selectedPlayerTwo) ||
            (!selectedPlayerOne && selectedPlayerTwo)
        ) {
            logError = "You need to select two players!";
            showError = true;

            setTimeout(() => {
                showError = false;
            }, 7000);
            return;
        }

        if (!winnerId || selectedPlayerOneId === selectedPlayerTwoId) {
            return;
        }

        const game = {
            playerOneId: selectedPlayerOneId,
            playerTwoId: selectedPlayerTwoId,
            // playerIds: [selectedPlayerOneId, selectedPlayerTwoId],
            // playerIds: [1, 2, 7, 8],
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
                showHand = false;
                const data = await response.json();
                playerOneRatingChange = data.playerOne.ratingDiff;
                playerTwoRatingChange = data.playerTwo.ratingDiff;
                gameFact = data.gameFact;
                showRevert = true;
                gameId = data.gameId;

                setTimeout(() => {
                    showAlert = false;
                }, 7000);

                setTimeout(() => {
                    showRevert = false;
                    selectedPlayerOne = undefined;
                    selectedPlayerTwo = undefined;
                    playerOneRatingChange = 0;
                    playerTwoRatingChange = 0;
                    winnerId = undefined;
                }, 10000);
            } else {
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
            <Alert.Title slot="title">Success</Alert.Title>
            <Alert.Description slot="description" class="italic"
                >{gameFact}</Alert.Description
            >
        </Alert>
    </div>
{/if}

{#if showError}
    <div class="alert" in:fly={{ y: 200, duration: 2000 }} out:fade>
        <Alert type="error">
            <Alert.Title slot="title">Error!</Alert.Title>
            <Alert.Description slot="description" class="italic"
                >{logError}</Alert.Description
            >
        </Alert>
    </div>
{/if}

<div class="flex flex-col items-center">
    <img
        src={billyLogo}
        alt="Pool sticks in cross"
        width="100px"
        class="mb-3"
    />

    <form class="w-2/4 max-w-md p-6 mx-auto bg-white rounded shadow-2xl">
        <div class="mb-6">
            <!-- Player one  -->
            <TheComboBox
                bind:player={selectedPlayerOneId}
                placeholder={"Player one"}
                players={data.mappedPlayers}
            />
        </div>

        <div class="mb-6">
            <!-- Player two -->
            <TheComboBox
                bind:player={selectedPlayerTwoId}
                placeholder={"Player two"}
                players={availablePlayers}
            />
        </div>
    </form>

    <div class="flex justify-center mt-10">
        <div class="w-1/2" style="position: relative;">
            {#if selectedPlayerOneId && selectedPlayerTwoId && showHand}
                <img
                    src={fingerPointing}
                    alt="fingerp pointing"
                    width="300"
                    style="rotate: 45deg; z-index: 1000; position: absolute; left: -190px; top: -70px; z-index: 100"
                />
            {/if}
            <Button
                on:click={() =>
                    handleWinnerSelect({
                        target: { value: selectedPlayerOneId },
                    })}
            >
                <div
                    class={`${
                        winnerId && winnerId === selectedPlayerOne?.id
                            ? "bg-green-200"
                            : ""
                    }  rounded shadow-md p-4 shadow-lg player-card `}
                >
                    <h2 class="mb-3 text-2xl font-bold">
                        {selectedPlayerOne?.name || "Player one"}
                    </h2>
                    <span class="flex">
                        <p>
                            Elo: {selectedPlayerOne?.rating || "N/A"}
                        </p>
                        {#if playerOneRatingChange}
                            <p
                                class={playerOneRatingChange > 0
                                    ? "green-text"
                                    : "red-text"}
                            >
                                {playerOneRatingChange > 0
                                    ? `+${playerOneRatingChange}`
                                    : `${playerOneRatingChange}`}
                            </p>
                        {/if}
                    </span>
                    <span class="flex">
                        <p class="flex">
                            Games: {selectedPlayerOne?.gamesPlayed || "N/A"}
                        </p>
                        {#if playerOneRatingChange}
                            <p class="green-text">+1</p>
                        {/if}
                    </span>
                </div>
            </Button>
        </div>

        <div class="flex items-center mx-4">
            <h1 class="text-4xl font-bold">VS</h1>
        </div>

        <div class="w-1/2">
            <Button
                on:click={() =>
                    handleWinnerSelect({
                        target: { value: selectedPlayerTwoId },
                    })}
            >
                <div
                    class={`${
                        winnerId && winnerId === selectedPlayerTwo?.id
                            ? "bg-green-200"
                            : ""
                    }  rounded shadow-md p-4 shadow-lg player-card `}
                >
                    <h2 class="mb-3 text-2xl font-bold">
                        {selectedPlayerTwo?.name || "Player two"}
                    </h2>
                    <span class="flex">
                        <p>
                            Elo: {selectedPlayerTwo?.rating || "N/A"}
                        </p>
                        {#if playerTwoRatingChange}
                            <p
                                class={playerTwoRatingChange > 0
                                    ? "green-text"
                                    : "red-text"}
                            >
                                {playerTwoRatingChange > 0
                                    ? `+${playerTwoRatingChange}`
                                    : `${playerTwoRatingChange}`}
                            </p>
                        {/if}
                    </span>
                    <span class="flex">
                        <p class="flex">
                            Games: {selectedPlayerTwo?.gamesPlayed || "N/A"}
                        </p>
                        {#if playerTwoRatingChange}
                            <p class="green-text">+1</p>
                        {/if}
                    </span>
                </div>
            </Button>
        </div>
    </div>

    <!-- Submit button -->
    <div class="text-center" style="margin-top: 20px">
        <!-- svelte-ignore a11y-missing-attribute -->
        <!-- svelte-ignore a11y-click-events-have-key-events -->
        <!-- svelte-ignore a11y-no-static-element-interactions -->
        <a
            class="relative inline-block text-lg group"
            style="width: 145px;"
            on:click={() => {
                if (!winnerId) {
                    return;
                }
                logGame();
                updateGameResultString();
            }}
        >
            <span
                class="relative z-10 block px-5 py-3 overflow-hidden font-medium leading-tight text-gray-800 transition-colors duration-300 ease-out border-2 border-gray-900 rounded-lg group-hover:text-white"
            >
                <span
                    class="absolute inset-0 w-full h-full px-5 py-3 rounded-lg bg-gray-50"
                />
                <span
                    class="absolute left-0 w-48 h-48 -ml-2 transition-all duration-300 origin-top-right -rotate-90 -translate-x-full translate-y-12 bg-gray-900 group-hover:-rotate-180 ease"
                />
                <span class="relative">{winnerId ? "Submit" : "Select"}</span>
            </span>
            <span
                class="absolute bottom-0 right-0 w-full h-12 -mb-1 -mr-1 transition-all duration-200 ease-linear bg-gray-900 rounded-lg group-hover:mb-0 group-hover:mr-0"
                data-rounded="rounded-lg"
            />
        </a>
    </div>
    {#if showRevert || isReverting}
        <div
            class="progress-bar"
            transition:fade={{ delay: 250, duration: 300 }}
        >
            <!-- <div class="progress-bar"> -->
            <TheProgressBar />
            <TheRevertButton bind:isReverting {gameId} />
        </div>
    {/if}
</div>

<style lang="scss">
    .progress-bar {
        margin-top: 20px;
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .alert {
        position: fixed;
        top: 10%;
        right: 20px;
        z-index: 1000;
        width: 400px;
    }

    .player-card {
        width: 200px;
        height: 150px;
        border: 3px solid black;
        border-radius: 15px;
        box-shadow:
            14px 14px 0 -4px black,
            14px 14px 0 0 black;

        p {
            font-size: 17px;
            text-align: left;
        }
    }

    .green-text {
        color: #629924;
    }

    .red-text {
        color: #cc3333;
    }
</style>
