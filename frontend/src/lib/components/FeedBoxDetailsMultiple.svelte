<script lang="ts">
    import type { PlayerSnapShot } from "$lib/interfaces";
    import { onMount } from "svelte";

    export let data: any;
    let players: PlayerSnapShot[] = [];

    onMount(() => {
        players = data.playerSnapshots;
    });
</script>

<div class="details-container shadow-lg">
    {#each players as player}
        <div class="details-content">
            <span style="display: flex;">
                <p style="margin-right: 10px; font-family: akira">
                    #{player.place}
                </p>
                <p>{player.name}</p>
                <p>
                    &nbsp{player.eloPre} =>
                </p>
                <p style="font-family: akira">
                    &nbsp{player.eloPost}
                </p>
                <p class={player.eloChange >= 0 ? "green-text" : "red-text"}>
                    &nbsp{player.eloChange >= 0
                        ? `+${player.eloChange}`
                        : player.eloChange}
                </p>
            </span>
        </div>
    {/each}
</div>

<style>
    .details-content {
        padding: 10px;
    }

    .green-text {
        color: #629924;
    }

    .red-text {
        color: #cc3333;
    }
</style>
