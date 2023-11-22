<script lang="ts">
    import TheFeedBox from "$lib/components/TheFeedBox.svelte";
    import type { PageData } from "./$types";

    export let data: PageData;
</script>

<section>
    <div class="shadow-2xl feed">
        {#each data.gamesPlayed as game}
            <TheFeedBox {game} showNumber />
        {/each}
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
</style>
