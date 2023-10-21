<script lang="ts">
    import { page } from "$app/stores";
    import { goto } from "$app/navigation";
    import { encodeSearchParams } from "stwui/utils";
    import { Button, Card, Table } from "stwui";
    import type { Player } from "$lib/interfaces";
    import { plus } from "$lib/assets/svg/svgPaths";
    import { Input } from "stwui";
    import { baseUrl } from "$lib/constants";
    import { columns } from "./TheTableColumns";
    import { formatDateTime } from "$lib/helpers/dates";

    let base_url: string;
    let orderBy: string;
    let order: "asc" | "desc";
    let playerName = "";

    export let players: Player[];

    $: {
        base_url = $page.url.pathname;
        orderBy = $page.url.searchParams.get("orderBy") || "created_at";
        order = $page.url.searchParams.get("order") === "desc" ? "desc" : "asc";
    }

    const goToPlayer = (id: number) => {
        window.sessionStorage.setItem("playerId", id.toString());
        goto(`players/${id}`, { replaceState: true });
    };

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

    function onColumnHeaderClick(column: string) {
        goto(
            `${base_url}` +
                encodeSearchParams({
                    orderBy: column,
                    order:
                        column === orderBy && order === "asc" ? "desc" : "asc",
                })
        );
    }
</script>

<Card bordered={false} class="h-[calc(100vh-14rem)]">
    <Card.Header
        slot="header"
        class="font-bold text-lg flex justify-between items-center py-3"
    >
        Players
        <div class="flex">
            <Input
                style="margin-right: 10px"
                name="input"
                placeholder="Name"
                bind:value={playerName}
            />
            <Button slot="extra" type="primary" on:click={addPlayer}>
                <Button.Leading slot="leading" data={plus} />
                Add player
            </Button>
        </div>
    </Card.Header>
    <Card.Content
        slot="content"
        class="p-0 sm:p-0"
        style="height: calc(100% - 64px);"
    >
        <Table class="rounded-md overflow-hidden h-full" {columns}>
            <Table.Header
                slot="header"
                {order}
                {orderBy}
                {onColumnHeaderClick}
            />
            <Table.Body slot="body">
                {#each players as item}
                    <Table.Body.Row
                        id={item.id.toString()}
                        on:click={() => {
                            goToPlayer(item.id);
                        }}
                    >
                        <Table.Body.Row.Cell column={0}
                            >{item.name}</Table.Body.Row.Cell
                        >
                        <Table.Body.Row.Cell column={1}
                            >{item.rating}</Table.Body.Row.Cell
                        >
                        <Table.Body.Row.Cell column={2}
                            >{item.gamesPlayed}</Table.Body.Row.Cell
                        >
                        <Table.Body.Row.Cell column={3}
                            >{item.wins}</Table.Body.Row.Cell
                        >
                        <Table.Body.Row.Cell column={4}
                            >{item.losses}</Table.Body.Row.Cell
                        >
                        <Table.Body.Row.Cell column={5}
                            >{item.winrate}%</Table.Body.Row.Cell
                        >
                        <Table.Body.Row.Cell column={6}
                            >{item.currentWinStreak}</Table.Body.Row.Cell
                        >
                        <Table.Body.Row.Cell column={7}
                            >{item.longestWinStreak}</Table.Body.Row.Cell
                        >
                        <Table.Body.Row.Cell column={8}
                            >{formatDateTime(
                                item.dateCreated
                            )}</Table.Body.Row.Cell
                        >
                    </Table.Body.Row>
                {/each}
            </Table.Body>
        </Table>
    </Card.Content>
</Card>
