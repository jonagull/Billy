<script lang="ts">
    import Chart from "$lib/components/Chart.svelte";
    import type { PageData } from "./$types";

    export let data: PageData;
</script>

<h1>Chart</h1>

<Chart data={data.playerElosLineData} />
