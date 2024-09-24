import { isMultipleTenant } from "$lib/constants";
import { fetchPageData } from "$lib/helpers/api";
import type { PlayerEloProgression } from "$lib/interfaces";
import type { PageLoad } from "./$types";

export const load = (async () => {
    let data

    if (isMultipleTenant) {
        data = await fetchPageData("players/elosMultiple");
    } else {
        data = await fetchPageData("players/elos");
    }


    if (data.length === 0) {
        return
    }

    const players = data.map((x: any) => {
        return {
            label: x.player.name,
            value: x.player.rating,
            id: x.player.id,
            disabled: false,
        };
    });

    const lengths = data.map((a: any) => a.length);
    lengths.indexOf(Math.max(...lengths));

    const longestArrayIndex = data
        .map((a: any) => a.elos.length)
        .indexOf(Math.max(...data.map((a: any) => a.elos.length)));

    const eloIndexLabels = data[longestArrayIndex].elos.map(
        (_: any, index: any) => index
    );

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

    const playerEloLineData = {
        labels: eloIndexLabels,
        datasets: data.map((x: PlayerEloProgression) => {
            return {
                label: x.player.name,
                fill: true,
                lineTension: 0.1,
                backgroundColor: "rgba(225, 204,230, .3)",
                borderColor: colors[Math.floor(Math.random() * colors.length)],
                borderCapStyle: "butt",
                borderDash: [],
                borderDashOffset: 0.0,
                borderJoinStyle: "miter",
                pointBorderColor: "rgb(205, 130,1 58)",
                pointBackgroundColor: "rgb(255, 255, 255)",
                pointBorderWidth: 1,
                pointHoverRadius: 1,
                pointHoverBackgroundColor: "rgb(0, 0, 0)",
                pointHoverBorderColor: "rgba(220, 220, 220,1)",
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: x.elos,
            };
        }),
    };

    return {
        playerElosLineData: playerEloLineData,
        players: players,
    };
}) satisfies PageLoad;
