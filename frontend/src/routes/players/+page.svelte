<script lang="ts">
    import type { PageData } from "./$types";
    import TheTable from "$lib/components/TheTable.svelte";

    export let data: PageData;
</script>

<div class="shadow-2xl">
    <TheTable players={data.players} />
</div>
