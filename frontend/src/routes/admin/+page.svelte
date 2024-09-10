<script lang="ts">
    import { onMount } from "svelte";
    import type { PageData } from "./$types";
    import ThePlayerEditForm from "$lib/components/ThePlayerEditForm.svelte";
    import type { Player } from "$lib/interfaces"; // Importing the Player interface

    export let data: PageData; // PageData should include players, an array of Player objects

    let selectedPlayer: Player | null = null; // Defining the type explicitly

    // Function to select a player by ID
    const selectPlayer = (id: number) => {
        selectedPlayer = data.players.find((p: Player) => p.id === id) || null;
    };
</script>

<h1>Admin</h1>

<!-- Iterate over the players array -->
{#each data.players as p (p.id)}
    <div on:click={() => selectPlayer(p.id)}>
        <p
            class={selectedPlayer && selectedPlayer.id === p.id
                ? "selected"
                : ""}
        >
            {p.name}
        </p>
    </div>
{/each}

<!-- Show ThePlayerEditForm when a player is selected -->
{#if selectedPlayer}
    <ThePlayerEditForm playerProp={selectedPlayer} />
{/if}

<style>
    .selected {
        background-color: lightblue;
        cursor: pointer;
    }
</style>
