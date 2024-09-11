import { baseUrl, isMultipleTenant } from "$lib/constants";
import { fetchPageData } from "$lib/helpers/api";
import type { Player, PlayerProfile } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async ({ params }) => {
    const playerResponse: PlayerProfile = await fetchPageData(
        "Players/" + params.id
    );

    let playerGameElos: number[] = [];
    let playerGamesWithSnapshots = [];

    const playersResponse: Player[] = await fetchPageData("Players");

    if (!isMultipleTenant) {
        playerGameElos = playerResponse.gamesPlayed.map((x) =>
            x.playerOne.id === +params.id ? x.playerOneElo : x.playerTwoElo
        );
    }

    if (isMultipleTenant) {
        const playerElos = await fetch(
            `${baseUrl}/Players/player/${params.id}/elo-history`
        );
        playerGameElos = await playerElos.json();

        const playerGamesPlayedWithSnapshotsResponse = await fetch(
            `${baseUrl}/Games/multiple/${params.id}`
        );

        playerGamesWithSnapshots =
            await playerGamesPlayedWithSnapshotsResponse.json();
    }

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

    const colors = [
        "rgb(205, 130, 158)",
        "rgb(130, 205, 158)",
        "rgb(158, 130, 205)",
        "rgb(205, 158, 130)",
        "rgb(130, 158, 205)",
        "rgb(158, 205, 130)",
        "rgb(205, 180, 130)",
        "rgb(130, 180, 205)",
        "rgb(180, 130, 205)",
        "rgb(205, 205, 130)",
        "rgb(130, 205, 180)",
        "rgb(205, 130, 180)",
        "rgb(180, 205, 130)",
        "rgb(130, 130, 205)",
        "rgb(205, 130, 130)",
    ];

    const opponentPieData = {
        labels: playerResponse.opponents.map((x) => x.name),
        datasets: [
            {
                data: playerResponse.opponents.map((x) => x.gamesAgainst),
                backgroundColor: [
                    "#F7464A",
                    "#46BFBD",
                    "#FDB45C",
                    "#949FB1",
                    "#4D5360",
                    "#AC64AD",
                ],
                hoverBackgroundColor: [
                    "#FF5A5E",
                    "#5AD3D1",
                    "#FFC870",
                    "#A8B3C5",
                    "#616774",
                    "#DA92DB",
                ],
            },
        ],
    };

    const opponentBarData = {
        labels: playerResponse.opponents.map((x) => x.name),
        datasets: [
            {
                label: "",
                data: playerResponse.opponents.map((x) => x.gamesAgainst),
                backgroundColor: [
                    "rgba(255, 134,159,0.4)",
                    "rgba(98,  182, 239,0.4)",
                    "rgba(255, 218, 128,0.4)",
                    "rgba(113, 205, 205,0.4)",
                    "rgba(170, 128, 252,0.4)",
                    "rgba(255, 177, 101,0.4)",
                ],
                borderWidth: 2,
                borderColor: [
                    "rgba(255, 134, 159, 1)",
                    "rgba(98,  182, 239, 1)",
                    "rgba(255, 218, 128, 1)",
                    "rgba(113, 205, 205, 1)",
                    "rgba(170, 128, 252, 1)",
                    "rgba(255, 177, 101, 1)",
                ],
            },
        ],
    };

    return {
        pageData: playerResponse,
        pId: params.id,
        playerGamesWithSnapshots: playerGamesWithSnapshots,
        opponentPieData: opponentPieData,
        opponentBarData: opponentBarData,
        gamesPlayed: playerResponse.gamesPlayed.reverse(),
        player: playerResponse.player,
        lineData: playerEloLineData,
        opponents: playerResponse.opponents,
        players: playersResponse.sort((a: any, b: any) =>
            a.name.localeCompare(b.name)
        ),
    };
}) satisfies PageLoad;
