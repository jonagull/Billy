<script lang="ts">
    import type { PageData } from "./$types";
    import TheTable from "$lib/components/TheTable.svelte";
    import TheButton from "$lib/components/TheButton.svelte";
    import { baseUrl } from "$lib/constants";
    import { invalidateAll } from "$app/navigation";

    export let data: PageData;

    let playerName = "";
    let dialog: HTMLDialogElement;

    function addPlayer() {
        const playerData = {
            name: playerName,
            rating: 1500,
            gamesPlayed: 0,
            dateCreated: new Date().toISOString(),
        };

        fetch(baseUrl + "/Players", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(playerData),
        })
            .then(async (response) => {
                if (response.ok) {
                    console.log("Player added successfully");
                    playerName = ""; // Reset the player name
                    invalidateAll();
                    dialog.close();
                } else {
                    console.error("Failed to add player");
                }
            })
            .catch((error) => {
                console.error(
                    "An error occurred while adding the player",
                    error
                );
            });
    }

    const openDialog = () => {
        dialog.showModal();
    };
</script>

<dialog
    class="dialog relative w-full max-w-md max-h-full rounded"
    bind:this={dialog}
>
    <div class="mb-4">
        <label
            class="block text-gray-700 text-sm font-bold mb-2"
            for="username"
        >
            Add player
        </label>
        <input
            bind:value={playerName}
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="username"
            type="text"
            placeholder="Player name"
        />

        <div class="mt-5">
            <button class="accept-button" type="button" on:click={addPlayer}>
                Add
            </button>

            <button
                class="cancel-button accept-button"
                type="button"
                on:click={() => dialog.close()}
            >
                Cancel
            </button>
        </div>
    </div>
</dialog>

<div class="flex mb-5 items-center" style="justify-content: space-between">
    <h1 class="mb-4 mr-5">Leaderboard</h1>
    <TheButton href="" label="Add Player" functionToCall={openDialog} />
</div>

<div class="shadow-2xl">
    <TheTable players={data.players} />
</div>

<style>
    .accept-button {
        background-color: rgb(32, 32, 32);
        border: none;
        color: white;
        padding: 10px 20px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        border-radius: 5px;
        cursor: pointer;
    }

    .cancel-button {
        background-color: rgb(255, 133, 133);
        color: black;
    }

    dialog::backdrop {
        -webkit-backdrop-filter: blur(2px);
        backdrop-filter: blur(2px);
    }
</style>
