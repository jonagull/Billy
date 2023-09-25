<script lang="ts">
    import type { PageData } from "./$types";
    import TheTable from "$lib/components/TheTable.svelte";

    export let data: PageData;

    const goToPlayer = (id: number) => {
        goto(`players/${id}`, { replaceState: true });
    };
</script>

<TheTable players={data.players} />
