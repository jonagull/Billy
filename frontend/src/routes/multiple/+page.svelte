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
    let selectedPlayerId: any;
    let playersCopy: any[] = [];
    let selectedPlayers: any[] = [];
    let playersRanking: any[] = [];

    onMount(() => {
        availablePlayers = data.mappedPlayers;
        playersCopy = data.mappedPlayers;
    });

    $: selectedPlayerId, addSelectedPlayer(selectedPlayerId);

    const addSelectedPlayer = (selectedPlayerId: number) => {
        if (!selectedPlayerId) {
            return;
        }

        if (selectedPlayers.find((player) => player.id === selectedPlayerId)) {
            return;
        }

        const selectedPlayer = data.players.find(
            (player: any) => player.id === selectedPlayerId
        );

        selectedPlayers.push(selectedPlayer);
        selectedPlayers = selectedPlayers;
        updatePlayerList();
    };

    const updatePlayerList = () => {
        playersCopy = data.mappedPlayers.map((x: any) => {
            return {
                id: x.id,
                label: x.label,
                value: x.value,
                disabled: selectedPlayers.map((x) => x.id).includes(x.id),
                rank: findPlayerRanking(x.id),
            };
        });
        playersCopy = playersCopy;
    };

    // $: {
    //     if (selectedPlayerOneId) {
    //         selectedPlayerOne = data.players.find(
    //             (player: { id: number | undefined }) =>
    //                 player.id === selectedPlayerOneId
    //         );

    //         updateAvailablePlayers();
    //     }

    //     if (selectedPlayerTwoId) {
    //         selectedPlayerTwo = data.players.find(
    //             (player: { id: number | undefined }) =>
    //                 player.id === selectedPlayerTwoId
    //         );
    //     }
    // }

    const updateAvailablePlayers = () => {
        availablePlayers = data.mappedPlayers.filter(
            (player: any) => player.id !== selectedPlayerOneId
        );
    };

    async function logGame() {
        if (playersRanking.length == 1) {
            logError = "You need at least two players to log a game!";
            showError = true;

            setTimeout(() => {
                showError = false;
            }, 7000);
            return;
        }

        try {
            const response = await fetch(baseUrl + "/games/multiple", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Accept: "application/json",
                },
                body: JSON.stringify({ playerIds: playersRanking }),
            });

            if (response.ok) {
                gameFact = "Looking good!";
                showAlert = true;
                showError = false;

                setTimeout(() => {
                    showAlert = false;
                }, 7000);
            } else {
                console.error("Failed to submit game.");
            }
        } catch (error) {
            console.error("An error occurred:", error);
        }
    }

    const removePlayer = (id: number) => {
        selectedPlayers = selectedPlayers.filter((player) => player.id !== id);

        if (selectedPlayers.length === 0) {
            return;
        }

        updatePlayerList();
        updateSelectedPlayersRanking(0);
    };

    const updateSelectedPlayersRanking = (id: number) => {
        // Updated all players
        if (id === 0) {
            selectedPlayers = selectedPlayers.map((player) => {
                return {
                    ...player,
                    rank: findPlayerRanking(player.id),
                };
            });
            return;
        }

        selectedPlayers = selectedPlayers.map((player) => {
            if (player.id === id) {
                return {
                    ...player,
                    rank: findPlayerRanking(id),
                };
            }

            return player;
        });
    };

    const handlePlayerRanking = (id: number) => {
        // debugger;
        // The order of the player in the array is the ranking
        if (playersRanking.includes(id)) {
            return;
        }

        playersRanking.push(id);
        updateSelectedPlayersRanking(id);
    };

    const removePlayerFromRanking = (id: number) => {
        const index = playersRanking.indexOf(id);
        playersRanking.splice(index, 1);
        updateSelectedPlayersRanking(0);
    };

    const findPlayerRanking = (id: number) => {
        if (!playersRanking.includes(id)) {
            return "";
        }
        const index = playersRanking.indexOf(id);
        return index + 1;
    };
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
            <TheComboBox
                placeholder="Add players"
                bind:player={selectedPlayerId}
                players={playersCopy || data.players}
            />
        </div>
    </form>

    <!-- <div class="flex buttons-wrapper">
        {#each selectedPlayers as sp}
            <button class="sp-button" on:click={() => removePlayer(sp.id)}>
                {sp.name}
            </button>
        {/each}
    </div> -->

    <!-- Submit button -->
    <div class="text-center" style="margin-top: 20px">
        <!-- svelte-ignore a11y-missing-attribute -->
        <!-- svelte-ignore a11y-click-events-have-key-events -->
        <!-- svelte-ignore a11y-no-static-element-interactions -->
        <a
            class="relative inline-block text-lg group"
            style="width: 145px;"
            on:click={() => {
                logGame();
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

    <div>
        {#each selectedPlayers as sp}
            <Button on:click={() => handlePlayerRanking(sp.id)}>
                <div
                    class={`${
                        sp.ranking === 1 ? "bg-green-200" : ""
                    }  rounded shadow-md p-4 shadow-lg player-card `}
                >
                    <span>
                        <p class="player-ranking">
                            {sp.rank || ""}
                        </p>
                        <p
                            on:click={() => {
                                removePlayer(sp.id),
                                    removePlayerFromRanking(sp.id);
                            }}
                            class="remove-button"
                        >
                            X
                        </p>
                    </span>
                    <h2 class="mb-3 text-2xl font-bold">
                        {sp.name || "Player two"}
                    </h2>
                    <span class="flex">
                        <p>
                            Elo: {sp.rating || "N/A"}
                        </p>
                        <!-- {#if playerTwoRatingChange}
                            <p
                                class={playerTwoRatingChange > 0
                                    ? "green-text"
                                    : "red-text"}
                            >
                                {playerTwoRatingChange > 0
                                    ? `+${playerTwoRatingChange}`
                                    : `${playerTwoRatingChange}`}
                            </p>
                        {/if} -->
                    </span>
                    <span class="flex">
                        <p class="flex">
                            Games: {sp.gamesPlayed || "N/A"}
                        </p>
                        {#if playerTwoRatingChange}
                            <p class="green-text">+1</p>
                        {/if}
                    </span>
                </div>
            </Button>
        {/each}
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
        text-overflow: ellipsis;
        overflow: hidden;
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

    .buttons-wrapper {
        margin-left: 260px;
        justify-content: center;
        align-items: flex-end;
    }
    .sp-button {
        position: relative;
        margin: 0 5px;
        background-color: rgb(215, 215, 215);
        border-radius: 20px;
        padding: 0px 20px;
        height: 40px;
    }

    .remove-button {
        position: absolute;
        top: 10px;
        right: 20px;
        padding: 5px;
        border-radius: 5px;
        font-family: "Akira";
    }

    .remove-button:hover {
        scale: 1.2;
    }

    .player-ranking {
        position: absolute;
        top: 10px;
        left: 20px;
        padding: 5px;
        border-radius: 5px;
        font-family: "Akira";
    }
</style>
