<script lang="ts">
    import { chart, dice, person } from "$lib/assets/svg/svgPaths";
    import type { GamePlayed } from "$lib/interfaces";
    import type { PageData } from "./$types";

    export let data: PageData;

    const getGameLineLabel = (game: GamePlayed) => {
        const label =
            game.winner.name === data.pageData.player.name
                ? `Vant over ${
                      game.winner.name === game.playerOne.name &&
                      game.playerOne.name === data.pageData.player.name
                          ? game.playerTwo.name
                          : game.playerOne.name
                  }`
                : `Tapte mot ${
                      game.winner.name !== game.playerOne.name &&
                      game.playerOne.name === data.pageData.player.name
                          ? game.playerTwo.name
                          : game.playerOne.name
                  }`;

        return label;
    };

    import { Tabs } from "stwui";

    interface Tab {
        href: string;
        title: string;
        data: string;
    }

    const tabs: Tab[] = [
        {
            href: "#tab1",
            title: "Metrics",
            data: person,
        },
        {
            href: "#tab2",
            title: "Games",
            data: dice,
        },
        {
            href: "#tab3",
            title: "Chart",
            data: chart,
        },
    ];

    let currentTab = "#tab1";
</script>

<h1 style="margin-bottom:10px">{data.pageData.player.name}</h1>

<Tabs {currentTab} variant="bar">
    {#each tabs as tab, i}
        <Tabs.Tab
            key={tab.href}
            href={tab.href}
            on:click={() => (currentTab = tab.href)}
        >
            <Tabs.Tab.Icon slot="icon" data={tab.data} />
            {tab.title}
        </Tabs.Tab>
    {/each}
</Tabs>

{#if currentTab === "#tab2"}
    <div class="feed" style="margin-top: 10px">
        {#each data.gamesPlayed as game}
            <div class="games">
                <span
                    style="display: flex; margin-bottom: 2px; height:20px; align-items: center"
                >
                    <p style="font-size: 16px;">
                        {`#${game.id} - ${getGameLineLabel(game)}`}
                    </p>
                </span>
                <p style="font-size: 14px; color: grey;">
                    {new Date(game.timeOfPlay).toLocaleString("en-US", {
                        timeZone: "Europe/Oslo",
                    })}
                </p>
            </div>
        {/each}
    </div>
{/if}

<style>
    .feed {
        margin: 0 300px;
        padding: 50px 0;
        display: flex;
        flex-direction: column;
        align-items: center;
        border: 1px solid grey;
        border-radius: 20px;
    }

    .games {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 20px;
    }

    * {
        color: black;
    }

    p {
        font-size: 18px;
    }
</style>
