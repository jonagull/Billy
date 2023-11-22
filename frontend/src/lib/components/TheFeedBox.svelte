<script lang="ts">
    import { formatFeedDate } from "$lib/helpers/dates";

    export let game: any;
    export let showNumber: boolean = false;
</script>

<div class="shadow-lg feed-element">
    <span
        style="display: flex; margin-bottom: 2px; height:20px; align-items: space-between; width: 100% "
    >
        <p class="feed-element-name">
            {`${
                game.winnerName === game.playerOneName
                    ? game.playerOneName
                    : game.playerTwoName
            }`}
        </p>
        <div style="position: relative;">
            {#if showNumber}
                <p class="id-box akira">#{game.id}</p>
            {/if}

            <p class="feed-time">
                {formatFeedDate(game.timeOfPlay)}
            </p>
        </div>
        <p class="feed-element-name">
            {`${
                game.winnerName === game.playerOneName
                    ? game.playerTwoName
                    : game.playerOneName
            }`}
        </p>
    </span>
</div>

<style>
    .akira {
        font-family: "Akira";
    }

    .id-box {
        position: absolute;
        top: -22px;
        right: 39%;
        font-size: 17px;
    }

    .feed-element {
        border-radius: 20px;
        width: 550px;
        padding: 10px;
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 20px;
        background: linear-gradient(90deg, #bbf7d0 50%, #fecaca 50%);
    }

    .feed-time {
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 14px;
        color: white;
        padding: 3px;
        width: 131px;
        border-radius: 5px;
        background-color: black;
    }

    .feed-element-name {
        display: flex;
        font-size: 16px;
        width: 50%;
        justify-content: center;
    }
</style>
