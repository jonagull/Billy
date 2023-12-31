<script lang="ts">
    import TheLineChart from "$lib/components/TheLineChart.svelte";
    import type { PageData } from "./$types";
    import TheComboBox from "$lib/components/TheComboBox.svelte";
    import { onMount } from "svelte";
    import { invalidateAll } from "$app/navigation";

    export let data: PageData;

    let selectedPlayers: any[] = [];
    let selectedPlayerId: any;
    let playerElosCopy: any;
    let playersCopy: any[] = [];

    $: selectedPlayerId, addSelectedPlayer(selectedPlayerId);

    onMount(() => {
        playerElosCopy = data.playerElosLineData;
        playersCopy = data.players;
    });

    const filterChartData = () => {
        let filtered: any[] = [];

        data.playerElosLineData.datasets.forEach((x: any) => {
            if (
                selectedPlayers.map((player) => player.label).includes(x.label)
            ) {
                filtered.push(x);
            }
        });

        // TODO: Fix this ples
        invalidateAll();

        playerElosCopy.datasets = filtered;
    };

    const addSelectedPlayer = (selectedPlayerId: number) => {
        console.log(selectedPlayerId);
        if (!selectedPlayerId) {
            return;
        }

        if (selectedPlayers.find((player) => player.id === selectedPlayerId)) {
            return;
        }

        const selectedPlayer = data.players.find(
            (player: any) => player.id === selectedPlayerId
        );

        selectedPlayers.push(selectedPlayer);
        selectedPlayers = selectedPlayers;
        filterChartData();
        updatePlayerList();
    };

    const removePlayer = (id: number) => {
        selectedPlayers = selectedPlayers.filter((player) => player.id !== id);

        updatePlayerList();

        if (selectedPlayers.length === 0) {
            playerElosCopy = data.playerElosLineData;
            return;
        }
        filterChartData();
    };

    const updatePlayerList = () => {
        playersCopy = data.players.map((x: any) => {
            return {
                id: x.id,
                label: x.label,
                value: x.value,
                disabled: selectedPlayers.map((x) => x.id).includes(x.id),
            };
        });
        playersCopy = playersCopy;
    };
</script>

<h1>Chart</h1>

<div class="flex">
    <div class="combobox-container flex">
        <TheComboBox
            placeholder="Players"
            bind:player={selectedPlayerId}
            players={playersCopy || data.players}
        />
    </div>
    <div class="flex buttons-wrapper">
        {#each selectedPlayers as sp}
            <button class="sp-button" on:click={() => removePlayer(sp.id)}>
                {sp.label}
            </button>
        {/each}
    </div>
</div>

<TheLineChart data={playerElosCopy || data.playerElosLineData} />

<style>
    .button-image {
        position: absolute;
        height: 10px;
        top: 7px;
        right: 10px;
    }
    .buttons-wrapper {
        margin-left: 260px;
        justify-content: center;
        align-items: flex-end;
    }
    .sp-button {
        position: relative;
        margin: 0 5px;
        background-color: rgb(215, 215, 215);
        border-radius: 20px;
        padding: 0px 20px;
        height: 40px;
    }
    .combobox-container {
        width: 100px;
    }
</style>
