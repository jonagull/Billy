<script lang="ts">
    import type { HomeFeed } from "$lib/interfaces";
    import { onMount } from "svelte";

    export let data: HomeFeed;

    let firstPlayerElo = 0;
    let firstPlayerNewElo = 0;
    let secondPlayerElo = 0;
    let secondtPlayerNewElo = 0;

    onMount(() => {
        if (data.game.winner.id === data.game.playerOne.id) {
            firstPlayerElo = data.game.playerOneElo;
            firstPlayerNewElo = data.eloChange.playerOneNewElo;
            secondPlayerElo = data.game.playerTwoElo;
            secondtPlayerNewElo = data.eloChange.playerTwoNewElo;
        } else {
            firstPlayerElo = data.game.playerTwoElo;
            firstPlayerNewElo = data.eloChange.playerTwoNewElo;
            secondPlayerElo = data.game.playerOneElo;
            secondtPlayerNewElo = data.eloChange.playerOneNewElo;
        }
    });
</script>

<div class="details-container shadow-lg">
    <div class="player-side">
        <p>{firstPlayerElo} -></p>
        <p class="new-elo-text">&nbsp{firstPlayerNewElo}</p>
    </div>

    <p style="font-family: 'Akira'">#{data.game.id}</p>

    <div class="player-side">
        <p>{secondPlayerElo} -></p>
        <p class="new-elo-text">&nbsp{secondtPlayerNewElo}</p>
    </div>
</div>

<style>
    .new-elo-text {
        font-weight: bold;
    }

    .details-container {
        height: 35px;
        padding: 10px;
        width: 470px;
        border-radius: 13px;
        display: flex;
        align-items: center;
    }

    .player-side {
        width: 50%;
        display: flex;
        justify-content: center;
    }
</style>
