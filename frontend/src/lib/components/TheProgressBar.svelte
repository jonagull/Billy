<script lang="ts">
    import { tweened } from "svelte/motion";
    import { onMount } from "svelte";

    let innerBarWidth = tweened(0);

    onMount(() => {
        innerBarWidth.set(100, { duration: 10000 });
    });
</script>

<div class="outerContainer">
    <div class="innerBar" style={`width: ${$innerBarWidth}%`} />
</div>

<style>
    .innerBar {
        background: black;
        height: 100%;
    }

    .outerContainer {
        width: 400px;
        height: 30px;
        border-radius: 5px;
        overflow: hidden;
        background: lightgray;
    }
</style>
