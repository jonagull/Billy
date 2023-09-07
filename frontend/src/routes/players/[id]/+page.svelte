<script lang="ts">
    import { chart, dice, person } from "$lib/assets/svg/svgPaths";
    import Chart from "$lib/components/Chart.svelte";
    import type { GamePlayed } from "$lib/interfaces";
    import type { PageData } from "./$types";
    import { Tabs } from "stwui";
    import { Input } from "stwui";

    export let data: PageData;

    let currentTab = "#tab3";

    let playerMetrics = [
        {
            label: "Antall kamper",
            value: data.player.gamesPlayed as unknown as string,
        },
        {
            label: "Rating",
            value: data.player.rating as unknown as string,
        },
        {
            label: "Wins",
            value: data.player.wins as unknown as string,
        },
        {
            label: "Loss",
            value: data.player.losses as unknown as string,
        },
        {
            label: "Winrate",
            value: (data.player.winrate as unknown as string) + "%",
        },
    ];

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

    const getGameLineLabel = (game: GamePlayed) => {
        const label =
            game.winner.name === data.player.name
                ? `Vant over ${
                      game.winner.name === game.playerOne.name &&
                      game.playerOne.name === data.player.name
                          ? game.playerTwo.name
                          : game.playerOne.name
                  }`
                : `Tapte mot ${
                      game.winner.name !== game.playerOne.name &&
                      game.playerOne.name === data.player.name
                          ? game.playerTwo.name
                          : game.playerOne.name
                  }`;

        return label;
    };
</script>

<h1 style="margin-bottom:10px">{data.player.name}</h1>

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

{#if currentTab === "#tab1"}
    <div style="margin-top: 10px">
        {#each playerMetrics as metric}
            <Input name="input" readonly={true} placeholder={metric.value}>
                <Input.Label slot="label">{metric.label}</Input.Label>
            </Input>
        {/each}
    </div>
{/if}

{#if currentTab === "#tab2"}
    <div class="feed" style="margin-top: 10px">
        {#each data.gamesPlayed as game}
            <div
                class={game.winner.name === data.player.name
                    ? "games game bg-green-200"
                    : "games game bg-red-200"}
            >
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

{#if currentTab === "#tab3"}
    <Chart data={data.lineData} />
{/if}

<style>
    .game {
        border: 1px solid black;
        border-radius: 10px;
        padding: 10px;
    }
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
