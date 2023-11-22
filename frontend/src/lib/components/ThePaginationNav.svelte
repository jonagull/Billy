<script lang="ts">
    import { goto } from "$app/navigation";
    import { createPagination, melt } from "@melt-ui/svelte";
    import { ChevronLeft, ChevronRight } from "lucide-svelte";

    export let totalGames: number;
    export let pageSize: number;
    export let currentPage: number;

    const {
        elements: { root, pageTrigger, prevButton, nextButton },
        states: { pages, range, page },
    } = createPagination({
        count: totalGames,
        perPage: pageSize,
        defaultPage: 1,
        siblingCount: 1,
    });

    $: $page, goto(`feed?page=${$page}&pageSize=10`, { replaceState: true });
</script>

<nav
    class="flex flex-col items-center gap-4 mt-5"
    aria-label="pagination"
    use:melt={$root}
>
    <!-- Not sure if I want to have this  -->
    <!-- <p class="text-center text-black-900">
        Showing items {$range.start} - {$range.end}
    </p> -->
    <div class="flex items-center gap-2">
        <button
            class="grid h-8 items-center rounded-md bg-white px-3 text-sm text-black-900 shadow-sm
        hover:opacity-75 disabled:cursor-not-allowed disabled:opacity-50 data-[selected]:bg-black-900
        data-[selected]:text-white"
            use:melt={$prevButton}><ChevronLeft class="square-4" /></button
        >
        {#each $pages as page (page.key)}
            {#if page.type === "ellipsis"}
                <span>...</span>
            {:else}
                <button
                    class="grid h-8 items-center rounded-md bg-white px-3 text-sm text-black-900 shadow-sm
            hover:opacity-75 disabled:cursor-not-allowed disabled:opacity-50 data-[selected]:bg-gray-900
          data-[selected]:text-white"
                    use:melt={$pageTrigger(page)}>{page.value}</button
                >
            {/if}
        {/each}
        <button
            class="grid h-8 items-center rounded-md bg-white px-3 text-sm text-magnum-900 shadow-sm
        hover:opacity-75 disabled:cursor-not-allowed disabled:opacity-50 data-[selected]:bg-magnum-900
      data-[selected]:text-white"
            use:melt={$nextButton}><ChevronRight class="square-4" /></button
        >
    </div>
</nav>
