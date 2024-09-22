<script lang="ts">
    import TheFeedBoxMultiple from "$lib/components/TheFeedBoxMutiple.svelte";
    import ThePaginationNav from "$lib/components/ThePaginationNav.svelte";
    import type { PageData } from "./$types";

    export let data: PageData;

    console.log("data", data);
</script>

<h1>Feed</h1>

<section>
    <div class="shadow-2xl feed">
        {#each data.gameWithSnapshots as game}
            <TheFeedBoxMultiple {game} />
        {/each}
    </div>

    <!-- <ThePaginationNav
        totalGames={data.totalGames}
        currentPage={data.currentPage}
        pageSize={data.pageSize}
    /> -->
</section>

<style>
    .feed {
        margin: 0 350px;
        padding: 50px 0;
        display: flex;
        flex-direction: column;
        align-items: center;
        border-radius: 20px;
        box-shadow:
            14px 14px 0 -4px black,
            14px 14px 0 0 black;
        border: 3px solid black;
    }
</style>
