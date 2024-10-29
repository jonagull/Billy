<script lang="ts">
  import { onMount } from "svelte";

  let dialog: HTMLDialogElement;

  export let snapshots: any;

  onMount(() => {
    dialog.showModal();
  });
  // export function openModal() {
  //     dialog.showModal();
  // }
</script>

<dialog
  class="dialog relative w-full max-w-md max-h-full rounded"
  bind:this={dialog}
>
  <!-- {#each snapshots as ss}
        <p>{ss.name}</p>
    {/each} -->
</dialog>

<style lang="scss">
  dialog::backdrop {
    -webkit-backdrop-filter: blur(2px);
    backdrop-filter: blur(2px);
  }
</style>
