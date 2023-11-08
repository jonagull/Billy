<script lang="ts">
    import crown from "$lib/assets/crown.png";
    import billyLogo from "$lib/assets//milkybilly.png";
    import type { PageData } from "./$types";
    import type { Player } from "$lib/interfaces";
    import { Alert, Button } from "stwui";
    import { onMount } from "svelte";
    import { fade, fly } from "svelte/transition";
    import TheComboBox from "$lib/components/TheComboBox.svelte";
    import { baseUrl } from "$lib/constants";

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
    let dialog: HTMLDialogElement;
    let gameResult: string | undefined;

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

    async function submitForm() {
        if (!winnerId || selectedPlayerOneId === selectedPlayerTwoId) {
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
        <div class="w-1/2">
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
                    <h2 class="mb-1 text-2xl font-bold">
                        {selectedPlayerOne?.name || "Player one"}
                    </h2>
                    <hr class="mb-2" />
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
                    <h2 class="mb-1 text-2xl font-bold">
                        {selectedPlayerTwo?.name || "Player two"}
                    </h2>
                    <hr class="mb-2" />
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

                dialog.showModal();
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
</div>

<style lang="scss">
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

        p {
            font-size: 17px;
            text-align: left;
        }
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
