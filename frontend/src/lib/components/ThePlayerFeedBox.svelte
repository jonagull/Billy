<script lang="ts">
    import type { GamePlayed } from "$lib/interfaces";

    export let data: any;

    const getGameLineLabel = (game: GamePlayed) => {
        const label =
            game.winner.name === data.player.name
                ? `Vant over ${
                      game.winner.name === game.playerOne.name &&
                      game.playerOne.name === data.player.name
                          ? game.playerTwo.name
                          : game.playerOne.name
                  }`
                : `Tapte mot ${
                      game.winner.name !== game.playerOne.name &&
                      game.playerOne.name === data.player.name
                          ? game.playerTwo.name
                          : game.playerOne.name
                  }`;

        return label;
    };
</script>

<div class="feed" style="margin-top: 10px">
    {#each data.gamesPlayed as game}
        <div
            class={game.winner.name === data.player.name
                ? "games game bg-green-200"
                : "games game bg-red-200"}
        >
            <span
                style="display: flex; margin-bottom: 2px; height:20px; align-items: center"
            >
                <p style="font-size: 16px;">
                    {`#${game.id} - ${getGameLineLabel(game)}`}
                </p>
            </span>
            <p style="font-size: 14px; color: grey;">
                {new Date(game.timeOfPlay).toLocaleString("en-US", {
                    timeZone: "Europe/Oslo",
                })}
            </p>
        </div>
    {/each}
</div>

<style>
    .feed {
        margin: 0 300px;
        padding: 50px 0;
        display: flex;
        flex-direction: column;
        align-items: center;
        border: 1px solid grey;
        border-radius: 20px;
    }

    .game {
        border: 1px solid black;
        border-radius: 10px;
        padding: 10px;
    }

    .games {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 20px;
    }
</style>
