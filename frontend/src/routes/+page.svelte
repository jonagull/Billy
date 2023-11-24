<script lang="ts">
    import man from "$lib/assets/milkybilly.png";
    import TheFeedBox from "$lib/components/TheFeedBox.svelte";
    import TheButton from "$lib/components/TheButton.svelte";
    import { quotes } from "$lib/data/quotes";

    import type { PageData } from "./$types";

    export let data: PageData;
    let randomIndex = Math.floor(Math.random() * quotes.length);
</script>

<section>
    <div
        class="grid max-w-screen-xl px-4 py-8 mx-auto lg:gap-8 xl:gap-0 lg:py-16 lg:grid-cols-12"
        style="padding-left: 20%"
    >
        <div class="hidden lg:mt-0 lg:col-span-5 lg:flex" style="width: 260px;">
            <img src={man} alt="man playing pool" />
        </div>

        <div class="flex flex-col mr-auto place-self-center lg:col-span-7">
            <h1
                class="place-self-start max-w-2xl mb-4 text-4xl font-extrabold tracking-tight leading-none md:text-5xl xl:text-6xl akira"
            >
                Billy
            </h1>
            <p class="text-gray-500 italic max-w-2xl font-light">
                {`"${quotes[randomIndex].quote}"`}
            </p>
            <p class="place-self-start max-w-2xl mb-2 mr-2">
                - {quotes[randomIndex].author}
            </p>
            <TheButton href={"game"} label={"Play game"} />
        </div>
    </div>

    <div class="shadow-2xl feed">
        {#each data.gamesPlayed as game}
            <TheFeedBox {game} isHomeFeed />
        {/each}

        <TheButton href={"feed"} label={"See more"} />
    </div>
</section>

<style>
    .feed {
        margin: 0 350px;
        padding: 50px 0;
        display: flex;
        flex-direction: column;
        align-items: center;
        border-radius: 20px;
    }

    .akira {
        font-family: "Akira";
    }
</style>
