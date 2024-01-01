<script lang="ts">
    import type { HomeFeed } from "$lib/interfaces";
    import { onMount } from "svelte";
    import playerPlaceholder from "$lib/assets/player-placeholder.png";

    export let data: HomeFeed;

    let firstPlayerElo = 0;
    let firstPlayerId = 0;
    let firstPlayerNewElo = 0;
    let secondPlayerElo = 0;
    let secondPlayerId = 0;
    let secondtPlayerNewElo = 0;

    onMount(() => {
        if (data.game.winner.id === data.game.playerOne.id) {
            firstPlayerElo = data.game.playerOneElo;
            firstPlayerNewElo = data.eloChange.playerOneNewElo;
            firstPlayerId = data.game.playerOne.id;
            secondPlayerElo = data.game.playerTwoElo;
            secondPlayerId = data.game.playerTwo.id;
            secondtPlayerNewElo = data.eloChange.playerTwoNewElo;
        } else {
            firstPlayerElo = data.game.playerTwoElo;
            firstPlayerId = data.game.playerTwo.id;
            firstPlayerNewElo = data.eloChange.playerTwoNewElo;
            secondPlayerElo = data.game.playerOneElo;
            secondPlayerId = data.game.playerOne.id;
            secondtPlayerNewElo = data.eloChange.playerOneNewElo;
        }
    });
</script>

<div class="details-container shadow-lg">
    <div class="player-side">
        <a href="/players/{firstPlayerId}">
            <img
                src={playerPlaceholder}
                alt="playerPlaceHolder"
                class="player-placeholder"
            /></a
        >
        <p>{firstPlayerElo} -></p>
        <p class="new-elo-text">&nbsp{firstPlayerNewElo}</p>
    </div>

    <p style="font-family: 'Akira'">#{data.game.id}</p>

    <div class="player-side">
        <a href="/players/{secondPlayerId}">
            <img
                src={playerPlaceholder}
                alt="playerPlaceHolder"
                class="player-placeholder"
            /></a
        >
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

    .player-placeholder {
        width: 20px;
        margin-right: 20px;
    }
</style>
