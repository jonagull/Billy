<script lang="ts">
    import { formatFeedDate } from "$lib/helpers/dates";
    import { createTooltip, melt } from "@melt-ui/svelte";
    import { fade } from "svelte/transition";
    import FeedBoxDetails from "./FeedBoxDetails.svelte";

    export let game: any;
    export let showNumber: boolean = false;
    export let isHomeFeed: boolean = false;

    const {
        elements: { trigger, content, arrow },
        states: { open },
    } = createTooltip({
        positioning: {
            placement: "top",
        },
        openDelay: 0,
        closeDelay: 0,
        closeOnPointerDown: false,
        forceVisible: true,
    });
</script>

{#if $open && isHomeFeed}
    <div
        use:melt={$content}
        transition:fade={{ duration: 100 }}
        class="z-10 rounded-lg bg-white shadow"
    >
        <div use:melt={$arrow} />

        <FeedBoxDetails data={game} />
    </div>
{/if}

<div class="shadow-lg feed-element">
    <span
        style="display: flex; margin-bottom: 2px; height:20px; align-items: space-between; width: 100%;"
        use:melt={$trigger}
    >
        <p class="feed-element-name">
            {`${
                isHomeFeed
                    ? game.game.winner.name === game.game.playerOne.name
                        ? game.game.playerOne.name
                        : game.game.playerTwo.name
                    : game.winnerName === game.playerOneName
                      ? game.playerOneName
                      : game.playerTwoName
            }`}
        </p>
        <div style="position: relative;">
            {#if showNumber}
                <p class="id-box akira">#{game.id}</p>
            {/if}

            <p class="feed-time">
                {formatFeedDate(
                    isHomeFeed ? game.game.timeOfPlay : game.timeOfPlay
                )}
            </p>
        </div>
        <p class="feed-element-name">
            {`${
                isHomeFeed
                    ? game.game.winner.name === game.game.playerOne.name
                        ? game.game.playerTwo.name
                        : game.game.playerOne.name
                    : game.winnerName === game.playerOneName
                      ? game.playerTwoName
                      : game.playerOneName
            }`}
        </p>
    </span>
</div>

<style lang="postcss">
    .trigger {
        @apply inline-flex h-9 w-9 items-center justify-center rounded-full bg-white;
        @apply text-magnum-900 transition-colors hover:bg-white/90;
        @apply focus-visible:ring focus-visible:ring-magnum-400 focus-visible:ring-offset-2;
        @apply p-0 text-sm font-medium;
    }

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
