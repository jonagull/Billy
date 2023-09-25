<script lang="ts">
    import type { PageData } from "./$types";
    import TheTable from "$lib/components/TheTable.svelte";

    export let data: PageData;
</script>

<TheTable players={data.players} />
