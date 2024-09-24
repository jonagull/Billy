<script lang="ts">
  import revertArrow from "$lib/assets/newdrawnarrow.png";
  import loader from "$lib/assets/loadingball.gif";
  import { baseUrl, isMultipleTenant } from "$lib/constants";
  import { fade } from "svelte/transition";

  export let isReverting: boolean = false;
  export let gameId: number;

  let loading: boolean = false;
  let showResponse: boolean = false;

  const revertGame = () => {
    loading = true;
    isReverting = true;
    const endpoint = isMultipleTenant
      ? `${baseUrl}/Games/RevertGameWithSnapshots/${gameId}`
      : `${baseUrl}/Games/revert/${gameId}`;

    fetch(endpoint, {
      method: "POST",
    })
      .then(async (response) => {
        if (response.ok) {
          console.log("Game reverted successfully");

          setTimeout(() => {
            showResponse = true;
          }, 2000);

          setTimeout(() => {
            isReverting = false;
          }, 10000);
        } else {
          console.error("Failed to revert game");
          setTimeout(() => {
            showResponse = true;
          }, 2000);

          setTimeout(() => {
            isReverting = false;
          }, 10000);
        }
      })
      .catch((error) => {
        console.error("An error occurred while reverting the game", error);
      });
  };
</script>

{#if !loading}
  <button on:click={revertGame}>
    <img class="arrow-image" src={revertArrow} alt="revert arrow" />
  </button>
{:else}
  <div>
    <img class="loader" src={loader} alt="loader" />
  </div>
{/if}

{#if !showResponse}
  <div class="mt-3">
    <p>Revert game if you need to</p>
  </div>
{/if}

{#if showResponse}
  <div class="mt-3">
    <p>Game reverted successfully</p>
  </div>
{/if}

<style>
  button {
    margin-top: 10px;
    background-color: black;
    border-radius: 23px;
    padding: 25px;
    width: 100px;
    height: 100px;
    cursor: pointer;
    display: flex;
    justify-content: center;
  }

  .loader {
    margin-top: 10px;
    background-color: black;
    border-radius: 23px;
    padding: 10px;
    width: 100px;
    height: 100px;
    cursor: pointer;
    display: flex;
    justify-content: center;
  }

  button:hover {
    scale: 1.1;
  }
</style>
