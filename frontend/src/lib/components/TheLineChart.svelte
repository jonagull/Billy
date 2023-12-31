<script>
    // @ts-nocheck
    import { Line } from "svelte-chartjs";

    export let data;

    import {
        Chart as ChartJS,
        Title,
        Tooltip,
        Legend,
        LineElement,
        LinearScale,
        PointElement,
        CategoryScale,
    } from "chart.js";

    ChartJS.register(
        Title,
        Tooltip,
        Legend,
        LineElement,
        LinearScale,
        PointElement,
        CategoryScale
    );
</script>

<Line {data} options={{ responsive: true }} />
