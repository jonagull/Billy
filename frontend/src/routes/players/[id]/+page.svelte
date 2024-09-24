<script lang="ts">
    import { chart, dice, person } from "$lib/assets/svg/svgPaths";
    import type { PageData } from "./$types";
    import { Tabs } from "stwui";
    import { Input } from "stwui";
    import { goto } from "$app/navigation";
    import TheLineChart from "$lib/components/TheLineChart.svelte";
    import ThePieChart from "$lib/components/ThePieChart.svelte";
    import TheBartChart from "$lib/components/TheBartChart.svelte";
    import ThePlayerFeedBox from "$lib/components/ThePlayerFeedBox.svelte";
    import { isMultipleTenant } from "$lib/constants";
    import TheFeedBoxMultiple from "$lib/components/TheFeedBoxMutiple.svelte";

    export let data: PageData;

    let playerId: any;
    let chartType: any = window.sessionStorage.getItem("chartType") || "pie";
    let currentTab = "#tab1";

    $: chartType, window.sessionStorage.setItem("chartType", chartType);

    $: playerMetrics = [
        {
            label: "Elo",
            value: data.player.rating as unknown as string,
        },
        {
            label: "Games played",
            value: data.player.gamesPlayed as unknown as string,
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
        {
            label: "Current winstreak",
            value: data.player.currentWinStreak as unknown as string,
        },
        {
            label: "Longest winstreak",
            value: data.player.longestWinStreak as unknown as string,
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

    const handlePlayerIdChange = () => {
        window.sessionStorage.setItem("playerId", playerId);
        goto("/players/" + playerId, { replaceState: true });
    };
</script>

<div>
    <h1 style="margin-bottom:10px">{data.player.name}</h1>

    <select
        style="margin: 10px; width: 300px"
        class="form-select block w-full p-2 border border-gray-300 rounded"
        bind:value={playerId}
        on:change={handlePlayerIdChange}
    >
        <option value={data.player.id}>{data.player.name}</option>
        {#each data.players as player}
            <option value={player.id}>{player.name}</option>
        {/each}
    </select>
</div>

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
        <div class="grid-container">
            {#each playerMetrics as metric}
                <div class="grid-item">
                    <Input
                        name="input"
                        readonly={true}
                        placeholder={metric.value}
                        style="font-size: 20px; "
                    >
                        <Input.Label style="font-weight: bold;" slot="label"
                            >{metric.label}</Input.Label
                        >
                    </Input>
                </div>
            {/each}
        </div>

        {#if data.opponents.length > 0}
            <hr style="margin-top: 20px" />

            <div
                style="display: flex; flex-direction: column; margin-top: 20px; justify-content: center; align-items: center"
            >
                <h2 style="font-size: 20px;  margin-right:auto;">
                    <strong style="font-family: akira">Opponents</strong>
                    <div style="display: flex; flex-direction: column">
                        <label>
                            <input
                                type="radio"
                                bind:group={chartType}
                                value="pie"
                            />
                            Pie Chart
                        </label>
                        <label>
                            <input
                                type="radio"
                                bind:group={chartType}
                                value="bar"
                            />
                            Bar Chart
                        </label>
                    </div>
                </h2>

                {#if chartType === "pie"}
                    <ThePieChart data={data.opponentPieData} />
                {/if}

                {#if chartType === "bar"}
                    <TheBartChart data={data.opponentBarData} />
                {/if}
            </div>
        {/if}
    </div>
{/if}

{#if currentTab === "#tab2"}
    {#if isMultipleTenant}
        <div class="feed" style="margin-top: 20px">
            {#each data.playerGamesWithSnapshots as game}
                <TheFeedBoxMultiple
                    isPlayerProfile={true}
                    playerId={data.pId}
                    {game}
                />
            {/each}
        </div>
    {/if}

    {#if !isMultipleTenant}
        <ThePlayerFeedBox {data} />
    {/if}
{/if}

{#if currentTab === "#tab3"}
    <TheLineChart data={data.lineData} />
{/if}

<style>
    .feed {
        margin: 0 300px;
        padding: 50px 0;
        display: flex;
        flex-direction: column;
        align-items: center;
        border-radius: 20px;
        box-shadow:
            14px 14px 0 -4px black,
            14px 14px 0 0 black;
        border: 3px solid black;
    }
    * {
        color: black;
    }

    p {
        font-size: 18px;
    }

    .grid-container {
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        gap: 16px;
    }

    .grid-item {
        padding: 16px;
        border: 1px solid #ddd;
        border-radius: 8px;
    }
</style>
