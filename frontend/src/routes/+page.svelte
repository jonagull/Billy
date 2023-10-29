<script lang="ts">
    import man from "$lib/assets/milkybilly.png";
    import { quotes } from "$lib/data/quotes";
    import type { PageData } from "./$types";

    export let data: PageData;
    let randomIndex = Math.floor(Math.random() * quotes.length);

    const formatFeedDate = (time: any) => {
        const date = new Date(time).toLocaleString("no-NO", {
            timeZone: "Europe/Oslo",
        });

        const [datePart, timePart] = date.split(",");

        if (datePart === undefined || timePart === undefined) {
            return;
        }

        const dateAndMonthSplit = datePart.split(".", 2);
        let dateAndMonthTogether = dateAndMonthSplit.join(".");
        const timeSplit = timePart.split(":", 2);
        const timeTogether = timeSplit.join(":");
        const splittedDate = dateAndMonthTogether.split(".");
        const splittedDateLastIndex = splittedDate[1];

        if (splittedDateLastIndex.length === 1) {
            const keksde = "0" + splittedDateLastIndex;
            dateAndMonthTogether = splittedDate[0] + "." + keksde;
        }

        return timeTogether + " - " + dateAndMonthTogether;
    };
</script>

<section>
    <div
        class="grid max-w-screen-xl px-4 py-8 mx-auto lg:gap-8 xl:gap-0 lg:py-16 lg:grid-cols-12"
        style="padding-left: 140px"
    >
        <div class="hidden lg:mt-0 lg:col-span-5 lg:flex" style="width: 260px;">
            <img src={man} alt="man playing pool" />
        </div>

        <div class="flex flex-col mr-auto place-self-center lg:col-span-7">
            <h1
                class="place-self-start max-w-2xl mb-4 text-4xl font-extrabold tracking-tight leading-none md:text-5xl xl:text-6xl"
            >
                Billy
            </h1>
            <p class="text-gray-500 italic max-w-2xl font-light">
                {`"${quotes[randomIndex].quote}"`}
            </p>
            <p class="place-self-start max-w-2xl mb-2 mr-2 text-gray-200">
                - {quotes[randomIndex].author}
            </p>
            <a
                href="/game"
                class="relative inline-block text-lg group"
                style="width: 145px;"
            >
                <span
                    class="relative z-10 block px-5 py-3 overflow-hidden font-medium leading-tight text-gray-800 transition-colors duration-300 ease-out border-2 border-gray-900 rounded-lg group-hover:text-white"
                >
                    <span
                        class="absolute inset-0 w-full h-full px-5 py-3 rounded-lg bg-gray-50"
                    />
                    <span
                        class="absolute left-0 w-48 h-48 -ml-2 transition-all duration-300 origin-top-right -rotate-90 -translate-x-full translate-y-12 bg-gray-900 group-hover:-rotate-180 ease"
                    />
                    <span class="relative">Play game</span>
                </span>
                <span
                    class="absolute bottom-0 right-0 w-full h-12 -mb-1 -mr-1 transition-all duration-200 ease-linear bg-gray-900 rounded-lg group-hover:mb-0 group-hover:mr-0"
                    data-rounded="rounded-lg"
                />
            </a>
        </div>
    </div>

    <div class="shadow-2xl feed">
        {#each data.gamesPlayed as game}
            <!-- svelte-ignore a11y-no-static-element-interactions -->
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
                    <p class="feed-time">
                        {formatFeedDate(game.timeOfPlay)}
                    </p>
                    <p class="feed-element-name">
                        {`${
                            game.winnerName === game.playerOneName
                                ? game.playerTwoName
                                : game.playerOneName
                        }`}
                    </p>
                </span>
            </div>
        {/each}
        <a
            href="/feed"
            class="relative inline-block text-lg group"
            style="width: 145px;"
        >
            <span
                class="relative z-10 block px-5 py-3 overflow-hidden font-medium leading-tight text-gray-800 transition-colors duration-300 ease-out border-2 border-gray-900 rounded-lg group-hover:text-white"
            >
                <span
                    class="absolute inset-0 w-full h-full px-5 py-3 rounded-lg bg-gray-50"
                />
                <span
                    class="absolute left-0 w-48 h-48 -ml-2 transition-all duration-300 origin-top-right -rotate-90 -translate-x-full translate-y-12 bg-gray-900 group-hover:-rotate-180 ease"
                />
                <span class="relative">See more</span>
            </span>
            <span
                class="absolute bottom-0 right-0 w-full h-12 -mb-1 -mr-1 transition-all duration-200 ease-linear bg-gray-900 rounded-lg group-hover:mb-0 group-hover:mr-0"
                data-rounded="rounded-lg"
            />
        </a>
    </div>
</section>

<style>
    .feed {
        margin: 0 200px;
        padding: 50px 0;
        display: flex;
        flex-direction: column;
        align-items: center;
        border: 1px solid black;
        border-radius: 20px;
    }

    .feed-element {
        border-radius: 20px;
        width: 550px;
        padding: 10px;
        border: 1px solid black;
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
