<script lang="ts">
  import {
    createCombobox,
    melt,
    type ComboboxOptionProps,
  } from "@melt-ui/svelte";
  import { Check, ChevronDown, ChevronUp } from "lucide-svelte";
  import { fly } from "svelte/transition";

  export let players: any[];
  export let placeholder: string = "Player";
  export let player: any | undefined;
  export let multiple: boolean = false;

  type Data = {
    id: number;
    label?: string;
    value: number;
    disabled: boolean;
  };

  const toOption = (players: Data): ComboboxOptionProps<Data> => ({
    value: players.id as any,
    label: players.label,
    disabled: players.disabled,
  });

  const {
    elements: { menu, input, option, label },
    states: { open, inputValue, touchedInput, selected },
    helpers: { isSelected },
  } = createCombobox<Data>({
    forceVisible: true,
    multiple: multiple as any,
  });

  $: if (!$open) {
    $inputValue = $selected?.label ?? "";
    player = $selected?.value ?? "";
  }

  $: filteredPlayers = $touchedInput
    ? players.filter(({ label }) => {
        const normalizedInput = $inputValue.toLowerCase();
        return label.toLowerCase().includes(normalizedInput);
      })
    : players;
</script>

<div class="flex flex-col gap-1">
  <!-- svelte-ignore a11y-label-has-associated-control - $label contains the 'for' attribute -->
  <label use:melt={$label}>
    <span class="text-sm font-medium">{placeholder}</span>
  </label>

  <div class="relative">
    <input
      style="width: 100%"
      use:melt={$input}
      class="flex h-10 items-center justify-between rounded-lg bg-white shadow-md
            px-3 pr-12 text-black"
      {placeholder}
    />
    <div class="absolute right-2 top-1/2 z-10 -translate-y-1/2 text-magnum-900">
      {#if $open}
        <ChevronUp class="square-4" />
      {:else}
        <ChevronDown class="square-4" />
      {/if}
    </div>
  </div>
</div>
{#if $open}
  <ul
    class="z-10 flex max-h-[300px] flex-col overflow-hidden rounded-lg"
    use:melt={$menu}
    transition:fly={{ duration: 150, y: -5 }}
  >
    <!-- svelte-ignore a11y-no-noninteractive-tabindex -->
    <div
      class="flex max-h-full flex-col gap-0 overflow-y-auto bg-white px-2 py-2 text-black"
      tabindex="0"
    >
      {#each filteredPlayers as player, index (index)}
        <li
          use:melt={$option(toOption(player))}
          class="relative cursor-pointer scroll-my-2 rounded-md py-2 pl-4 pr-4
                    hover:bg-magnum-100
                    data-[highlighted]:bg-slate-100 data-[highlighted]:text-black
                      data-[disabled]:opacity-50"
        >
          {#if $isSelected(player)}
            <div class="check absolute left-2 top-1/2 z-10 text-black">
              <Check class="square-4" />
            </div>
          {/if}
          <div class="pl-4">
            <span class="font-medium">{player.label}</span>
            <span class="block text-sm opacity-75">{player.value}</span>
          </div>
        </li>
      {:else}
        <li class="relative cursor-pointer rounded-md py-1 pl-8 pr-4">
          No results found
        </li>
      {/each}
    </div>
  </ul>
{/if}

<style lang="postcss">
  .check {
    @apply absolute left-2 top-1/2 text-magnum-500;
    translate: 0 calc(-50% + 1px);
  }

  .combobox-container {
    border: 1px solid black;
  }
</style>
