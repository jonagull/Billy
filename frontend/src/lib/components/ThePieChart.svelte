<script lang="ts">
    import { Pie } from "svelte-chartjs";

    export let data: any;

    import {
        Chart as ChartJS,
        Title,
        Tooltip,
        Legend,
        ArcElement,
        CategoryScale,
    } from "chart.js";

    ChartJS.register(Title, Tooltip, Legend, ArcElement, CategoryScale);
</script>

<div class="pie-wrapper">
    <Pie {data} options={{ responsive: true }} />
</div>

<style>
    .pie-wrapper {
        width: 50%;
        height: 50%;
    }
</style>
