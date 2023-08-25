<script lang="ts">
    import type { GamePlayed } from "$lib/interfaces";
    import type { PageData } from "./$types";

    export let data: PageData;

    const getGameLineLabel = (game: GamePlayed) => {
        const label =
            game.winner.name === data.pageData.player.name
                ? `Vant over ${
                      game.winner.name === game.playerOne.name &&
                      game.playerOne.name === data.pageData.player.name
                          ? game.playerTwo.name
                          : game.playerOne.name
                  }`
                : `Tapte mot ${
                      game.winner.name !== game.playerOne.name &&
                      game.playerOne.name === data.pageData.player.name
                          ? game.playerTwo.name
                          : game.playerOne.name
                  }`;

        return label;
    };
</script>

<h1 style="margin-bottom:10px">{data.pageData.player.name}</h1>

<h2 style="display: flex; justify-content: center; margin-bottom: 5px">
    Kamper spillt av {data.pageData.player.name}:
</h2>

<div class="feed">
    {#each data.pageData.gamesPlayed.reverse() as game}
        <div class="games">
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

    .games {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 20px;
    }

    * {
        color: black;
    }

    p {
        font-size: 18px;
    }
</style>
