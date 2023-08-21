<script lang="ts">
    import type { PageData } from "./$types";

    export let data: PageData;
</script>

<section>
    <div class="feed">
        {#each data.gamesPlayed as game}
            <div class="games">
                <span
                    style="display: flex; margin-bottom: 2px; height:20px; align-items: center"
                >
                    <p style="font-size: 16px;">
                        #{`${game.id} -                         ${
                            game.winnerName
                        } slo ${
                            game.winnerName === game.playerOneName
                                ? game.playerTwoName
                                : game.playerOneName
                        }`}
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
</section>

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
