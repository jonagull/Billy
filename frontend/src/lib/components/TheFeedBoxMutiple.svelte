<script lang="ts">
    import type { GameWithSnapshots, PlayerSnapShot } from "$lib/interfaces";
    import { createTooltip, melt } from "@melt-ui/svelte";
    import { fade } from "svelte/transition";
    import FeedBoxDetailsMultiple from "./FeedBoxDetailsMultiple.svelte";

    export let game: GameWithSnapshots;
    export let isPlayerProfile: boolean = false;
    export let playerId: string | null = null;

    const findWinnerName = (game: any) => {
        const snapshots = game.playerSnapshots;
        const winner = snapshots.find(
            (snapshot: PlayerSnapShot) => snapshot.place == 1
        );

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
            console.log("game", game);

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
    <p class="id-box akira">#{game.gameId}</p>
    <p>
        {findWinnerName(game)}
    </p>
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
</style>
