import { fetchPageData } from "$lib/helpers/api";
import type { Player, PlayerProfile } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async ({ params }) => {
    const playerResponse: PlayerProfile = await fetchPageData(
        "Players/" + params.id
    );
    const playersResponse: Player[] = await fetchPageData("Players");

    const playerGameElos = playerResponse.gamesPlayed.map((x) =>
        x.playerOne.id === +params.id ? x.playerOneElo : x.playerTwoElo
    );

    playerGameElos.push(playerResponse.player.rating);

    const playerGameLabels = playerGameElos.map((_, index) => index);

    const playerEloLineData = {
        labels: playerGameLabels,
        datasets: [
            {
                label: playerResponse.player.name + " elo history",
                fill: true,
                lineTension: 0.3,
                backgroundColor: "rgba(225, 204,230, .3)",
                borderColor: "rgb(205, 130, 158)",
                borderCapStyle: "butt",
                borderDash: [],
                borderDashOffset: 0.0,
                borderJoinStyle: "miter",
                pointBorderColor: "rgb(205, 130,1 58)",
                pointBackgroundColor: "rgb(255, 255, 255)",
                pointBorderWidth: 5,
                pointHoverRadius: 5,
                pointHoverBackgroundColor: "rgb(0, 0, 0)",
                pointHoverBorderColor: "rgba(220, 220, 220,1)",
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: playerGameElos,
            },
        ],
    };

    return {
        pageData: playerResponse,
        gamesPlayed: playerResponse.gamesPlayed.reverse(),
        player: playerResponse.player,
        lineData: playerEloLineData,
        players: playersResponse.sort((a: any, b: any) =>
            a.name.localeCompare(b.name)
        ),
    };
}) satisfies PageLoad;
