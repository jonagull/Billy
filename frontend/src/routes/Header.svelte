<script lang="ts">
    import { page } from "$app/stores";
    import github from "$lib/assets/github.svg";
    import { isMultipleTenant } from "$lib/constants";
    import { onMount } from "svelte";

    let playerId: any;

    let routes = [
        {
            name: "Home",
            path: "/",
        },
        {
            name: "Game",
            path: "/game",
        },
        {
            name: "Players",
            path: "/players?orderBy=rating&order=desc",
        },
        {
            name: "Profiles",
            path: `/players/1`,
        },
        {
            name: "Chart",
            path: "/chart",
        },
        {
            name: "Feed",
            path: "/feed",
        },
    ];

    let routesMultiple = [
        {
            name: "Home",
            path: "/",
        },
        {
            name: "Feed",
            path: "/feedmultiple",
        },
        {
            name: "Log Game",
            path: "/multiple",
        },
        {
            name: "Leaderboard",
            path: "/players?orderBy=rating&order=desc",
        },
        {
            name: "Profiles",
            path: `/players/7`,
        },
        {
            name: "Chart",
            path: "/chart",
        },
    ];

    onMount(() => {
        playerId = sessionStorage.getItem("playerId") || "1";
    });

    $: {
        playerId;
    }
</script>

<header>
    <div class="corner" />

    <nav>
        <svg viewBox="0 0 2 3" aria-hidden="true">
            <path d="M0,0 L1,2 C1.5,3 1.5,3 2,3 L2,0 Z" />
        </svg>
        <ul>
            {#each isMultipleTenant ? routesMultiple : routes as route}
                <li
                    aria-current={$page.url.pathname === route.path
                        ? "page"
                        : undefined}
                >
                    <a href={route.path}>{route.name}</a>
                </li>
            {/each}
        </ul>
        <svg viewBox="0 0 2 3" aria-hidden="true">
            <path d="M0,0 L0,3 C0.5,3 0.5,3 1,2 L2,0 Z" />
        </svg>
    </nav>

    <div class="corner">
        <!-- <a href="https://github.com/jonagull/pool-elo" target="_blank">
            <img src={github} alt="GitHub" />
        </a> -->
    </div>
</header>

<style>
    header {
        display: flex;
        justify-content: space-between;
    }

    .corner {
        width: 3em;
        height: 3em;
    }

    .corner a {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100%;
        height: 100%;
    }

    .corner img {
        width: 2em;
        height: 2em;
        object-fit: contain;
    }

    nav {
        display: flex;
        justify-content: center;
        --background: rgba(255, 255, 255, 0.7);
    }

    svg {
        width: 2em;
        height: 3em;
        display: block;
    }

    path {
        fill: var(--background);
    }

    ul {
        position: relative;
        padding: 0;
        margin: 0;
        height: 3em;
        display: flex;
        justify-content: center;
        align-items: center;
        list-style: none;
        background: var(--background);
        background-size: contain;
    }

    li {
        position: relative;
        height: 100%;
    }

    li[aria-current="page"]::before {
        --size: 6px;
        content: "";
        width: 0;
        height: 0;
        position: absolute;
        top: 0;
        left: calc(50% - var(--size));
        border: var(--size) solid transparent;
        border-top: var(--size) solid var(--color-theme-1);
    }

    nav a {
        display: flex;
        height: 100%;
        align-items: center;
        padding: 0 0.5rem;
        color: var(--color-text);
        font-weight: 700;
        font-size: 0.8rem;
        text-transform: uppercase;
        letter-spacing: 0.1em;
        text-decoration: none;
        transition: color 0.2s linear;
    }

    a:hover {
        color: var(--color-theme-1);
    }
</style>
