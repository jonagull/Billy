<script lang="ts">
    import { page } from "$app/stores";
    import "../app.postcss";
    import Header from "./Header.svelte";
    import "./styles.css";

    let isLaunchpad: boolean;
    $: isLaunchpad = $page.url.pathname.startsWith("/launchpad");
</script>

<div class="app">
    <!-- {#if !isLaunchpad} -->
    <Header />
    <!-- {/if} -->

    <main>
        <slot />
    </main>
</div>

<style>
    .app {
        display: flex;
        flex-direction: column;
        min-height: 100vh;
    }

    main {
        flex: 1;
        display: flex;
        flex-direction: column;
        padding: 1rem;
        width: 100%;
        max-width: 100rem;
        margin: 0 auto;
        box-sizing: border-box;
    }
</style>
