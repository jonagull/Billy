<script lang="ts">
    import { Input } from "stwui";
    import { Button } from "stwui";

    interface Player {
        id: number;
        name: string;
        elo: number;
        gamesPlayed: number;
    }

    interface GamePlayed {
        player1: Player;
        player2: Player;
        winner: Player;
    }

    let playerName = "";
    let players: Player[] = [];

    const addPlayer = () => {
        if (!playerName) {
            return;
        }

        console.log("playername", playerName);
        const id = players.length + 1;
        players = [
            ...players,
            { id: id, name: playerName, elo: 1200, gamesPlayed: 0 },
        ];
        playerName = ""; // Reset playerName to an empty string
        console.log(players);
    };

    const removePlayer = (index: number) => {
        players = players.filter((_, i) => i !== index);
    };
</script>

<main>
    <h1>Players</h1>

    <div class="flex mb-20">
        <Input name="input" placeholder="Name" bind:value={playerName} />

        <Button on:click={addPlayer} type="primary">Add</Button>
    </div>

    <h2>Players</h2>
    <ul>
        {#each players as player, index (player.id)}
            <li>
                <span class="flex">
                    <Input readonly name="input" bind:value={player.name} />
                    <Button on:click={() => removePlayer(index)} type="danger"
                        >Remove</Button
                    >
                </span>
            </li>
        {/each}
    </ul>
</main>

<style>
    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        margin-bottom: 10px;
    }
</style>
