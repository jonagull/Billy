<script lang="ts">
    import man from "$lib/assets/man.png";
    import type { PageData } from "./$types";

    export let data: PageData;
</script>

<section>
    <div
        class="grid max-w-screen-xl px-4 py-8 mx-auto lg:gap-8 xl:gap-0 lg:py-16 lg:grid-cols-12"
    >
        <div class="hidden lg:mt-0 lg:col-span-5 lg:flex">
            <img src={man} alt="man playing pool" />
        </div>
        <div class="mr-auto place-self-center lg:col-span-7">
            <h1
                class="max-w-2xl mb-4 text-4xl font-extrabold tracking-tight leading-none md:text-5xl xl:text-6xl"
            >
                Billy's pool
            </h1>
            <p
                class="max-w-2xl mb-6 font-light text-gray-500 lg:mb-8 md:text-lg lg:text-xl"
            >
                You dare challenge me?!
            </p>
            <a
                href="/game"
                class="inline-flex items-center justify-center px-5 py-3 mr-3 text-base font-medium text-center text-white rounded-lg bg-primary-700 hover:bg-primary-800 focus:ring-4 focus:ring-primary-300 dark:focus:ring-primary-900"
            >
                <button
                    class="flex items-center bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                >
                    Play game
                    <svg
                        class="w-5 h-5 ml-2 -mr-1"
                        fill="currentColor"
                        viewBox="0 0 20 20"
                        xmlns="http://www.w3.org/2000/svg"
                        ><path
                            fill-rule="evenodd"
                            d="M10.293 3.293a1 1 0 011.414 0l6 6a1 1 0 010 1.414l-6 6a1 1 0 01-1.414-1.414L14.586 11H3a1 1 0 110-2h11.586l-4.293-4.293a1 1 0 010-1.414z"
                            clip-rule="evenodd"
                        /></svg
                    >
                </button>
            </a>
        </div>
    </div>

    <div class="feed">
        {#each data.gamesPlayed as game}
            <div class="game">
                <p>
                    #{game.id} -

                    {game.winnerName} slo {game.winnerName ===
                    game.playerOneName
                        ? game.playerTwoName
                        : game.playerOneName} |
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
        margin: 0 150px;
        padding: 50px 0;
        display: flex;
        flex-direction: column;
        align-items: center;
        border: 1px solid black;
        border-radius: 20px;
    }
    * {
        color: black;
    }

    p {
        font-size: 18px;
        margin-bottom: 30px;
    }
</style>
