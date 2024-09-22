<script lang="ts">
    import type { GameWithSnapshots, PlayerSnapShot } from "$lib/interfaces";
    import { createTooltip, melt } from "@melt-ui/svelte";
    import { fade } from "svelte/transition";
    import FeedBoxDetailsMultiple from "./FeedBoxDetailsMultiple.svelte";
    import { onMount } from "svelte";
    import { formatFeedDate } from "$lib/helpers/dates";

    export let game: GameWithSnapshots;
    export let isPlayerProfile: boolean = false;
    export let playerId: string | null = null;

    const findWinnerName = (game: any) => {
        const winner = game.playerSnapshots.find(
            (snapshot: PlayerSnapShot) => snapshot.place == 1
        );
        if (!winner) {
            return "No winner";
        }
        return winner.name;
    };

    const {
        elements: { trigger, content, arrow },
        states: { open },
    } = createTooltip({
        positioning: {
            placement: "top",
        },
        openDelay: 50,
        closeDelay: 0,
        closeOnPointerDown: false,
        forceVisible: true,
    });

    const getClassName = () => {
        if (isPlayerProfile && playerId) {
            const place =
                game.playerSnapshots?.find((x) => x.playerId == +playerId)
                    ?.place || null;

            if (place !== 1) {
                return "feed-element red";
            }
        }

        return "feed-element";
    };
</script>

<div class={getClassName()} use:melt={$trigger}>
    <span style="display: flex">
        <p class="akira">#{game.gameId}&nbsp</p>
        <p>
            {findWinnerName(game)}&nbsp
        </p>
        <!-- <p class="feed-time" style="margin-left: 10px">
            {formatFeedDate(game.timeOfPlay)}
        </p> -->
    </span>
</div>

{#if $open}
    <div
        use:melt={$content}
        transition:fade={{ duration: 100 }}
        class="z-10 rounded-lg bg-white shadow"
    >
        <div use:melt={$arrow} />

        <FeedBoxDetailsMultiple data={game} />
    </div>
{/if}

<style>
    .id-box {
        position: absolute;
        top: -22px;
        right: 39%;
        font-size: 17px;
    }

    .feed-element {
        border-radius: 20px;
        width: 300px;
        padding: 10px;
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 20px;
        background: #bbf7d0;
    }

    .red {
        background-color: #fecaca;
    }

    .feed-element-name {
        display: flex;
        font-size: 16px;
        width: 50%;
        justify-content: center;
    }

    .akira {
        font-family: "Akira";
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
</style>
