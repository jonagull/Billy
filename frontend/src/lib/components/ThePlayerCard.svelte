<script lang="ts">
  //   import { PlayerSnapShot } from "$lib/interfaces";

  export let player;

  console.log("player", player);
</script>

<div class="rounded shadow-md p-4 shadow-lg player-card">
  <h2 class="mb-3 text-2xl font-bold">
    {player.name || "Player"}
  </h2>
</div>

<style lang="scss">
  .player-card {
    width: 100px;
    height: 75px;
    border: 3px solid black;
    border-radius: 15px;
    text-overflow: ellipsis;
    overflow: hidden;
    box-shadow:
      14px 14px 0 -4px black,
      14px 14px 0 0 black;

    p {
      font-size: 17px;
      text-align: left;
    }
  }
</style>
