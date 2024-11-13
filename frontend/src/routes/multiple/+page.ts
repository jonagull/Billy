import { fetchPageData } from "$lib/helpers/api";
import type { Player } from "$lib/interfaces";
import { last } from "@melt-ui/svelte/internal/helpers";
import { handleFetch } from "../../hooks";
import type { PageLoad } from "./$types";

export const load = (async () => {
  const response: Player[] = await handleFetch("/players");
  const lastGamePlayed = await handleFetch("/games/lastGamePlayed");

  console.log("lastgame", lastGamePlayed);

  const players = response.map((player) => {
    return {
      label: player.name,
      value: player.rating,
      id: player.id,
      disabled: false,
    };
  });

  return {
    recentPlayers: lastGamePlayed.playerSnapshots,
    players: response.sort((a: any, b: any) => a.name.localeCompare(b.name)),
    mappedPlayers: players.sort((a: any, b: any) =>
      a.label.localeCompare(b.label)
    ),
  };
}) satisfies PageLoad;
