<script lang="ts">
    import { baseUrl } from "$lib/constants";
    import { onMount } from "svelte";
    import { Alert, Button } from "stwui";
    import { fade, fly } from "svelte/transition";

    export let playerProp: any;
    let showAlert = false;

    // Local state to hold the player data
    let player = { ...playerProp }; // Copy the prop initially

    // Watch for changes in `playerProp` and update only when it changes
    $: if (playerProp && playerProp.id !== player.id) {
        player = { ...playerProp }; // Update local `player` only when `playerProp` changes
    }

    // Function to handle form submission
    const handleSubmit = async () => {
        const response = await fetch(baseUrl + "/players/" + player.id, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
            },
            body: JSON.stringify(player),
        });

        if (response.ok) {
            console.log("Player updated successfully");
            showAlert = true;

            setTimeout(() => {
                showAlert = false;
            }, 7000);
        } else {
            console.error("Failed to update player");
        }
    };
</script>

{#if showAlert}
    <div class="alert" in:fly={{ y: 200, duration: 2000 }} out:fade>
        <Alert type="success">
            <Alert.Title slot="title">Success</Alert.Title>
            <Alert.Description slot="description" class="italic">
                Player Updated
            </Alert.Description>
        </Alert>
    </div>
{/if}

<!-- Form to edit the player -->
<form on:submit|preventDefault={handleSubmit}>
    <div>
        <label for="name">Name:</label>
        <input type="text" id="name" bind:value={player.name} />
    </div>

    <div style="display: flex">
        <span>
            <label for="rating">Rating:</label>
            <input type="number" id="rating" bind:value={player.rating} />
        </span>

        <span>
            <label for="gamesPlayed">Games Played:</label>
            <input
                type="number"
                id="gamesPlayed"
                bind:value={player.gamesPlayed}
            />
        </span>
    </div>

    <div style="display: flex">
        <span>
            <label for="wins">Wins:</label>
            <input type="number" id="wins" bind:value={player.wins} />
        </span>

        <span>
            <label for="losses">Losses:</label>
            <input type="number" id="losses" bind:value={player.losses} />
        </span>
    </div>

    <div style="display: flex">
        <span>
            <label for="winrate">Winrate:</label>
            <input type="number" id="winrate" bind:value={player.winrate} />
        </span>

        <span>
            <label for="currentWinStreak">Current Streak:</label>
            <input
                type="number"
                id="currentWinStreak"
                bind:value={player.currentWinStreak}
            />
        </span>

        <span>
            <label for="longestWinStreak">Longest Streak:</label>
            <input
                type="number"
                id="longestWinStreak"
                bind:value={player.longestWinStreak}
            />
        </span>
    </div>

    <button type="submit">Update Player</button>
</form>

<style>
    .alert {
        position: fixed;
        top: 10%;
        right: 20px;
        z-index: 1000;
        width: 400px;
    }

    span {
        margin-right: 1rem;
    }

    form {
        max-width: 400px;
        margin: auto;
        display: flex;
        flex-direction: column;
    }
    div {
        margin-bottom: 1rem;
    }
    label {
        margin-bottom: 0.5rem;
    }
    input {
        width: 100%;
        padding: 0.5rem;
        border: 1px solid #ccc;
        border-radius: 4px;
    }
    button {
        padding: 0.5rem 1rem;
        background-color: #007bff;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }
    button:hover {
        background-color: #0056b3;
    }
</style>
