<script lang="ts">
    export let data: any;
</script>

<div class="details-container shadow-lg">
    <div class="player-side">
        <p>{data.game.playerOneElo} -></p>
        <p class="new-elo-text">&nbsp{data.eloChange.playerOneNewElo}</p>
    </div>

    <p style="font-family: 'Akira'">#{data.game.id}</p>

    <div class="player-side">
        <p>{data.game.playerTwoElo} -></p>
        <p class="new-elo-text">&nbsp{data.eloChange.playerTwoNewElo}</p>
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
