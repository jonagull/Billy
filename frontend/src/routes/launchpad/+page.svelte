<script>
    import billyLogo from "$lib/assets//milkybilly.png";
    import { fade, fly } from "svelte/transition";
</script>

<div class="container">
    <div class="billy-container">
        <img
            src={billyLogo}
            alt="Pool sticks in cross"
            width="200px"
            class="mb-3"
        />

        <h1 in:fly={{ y: -200, duration: 1500 }}>Billy</h1>
    </div>
    <div class="launchpad">
        <a href="multiple" class="a">Log Game</a>
        <a href="players?orderBy=rating&order=desc" class="a">Leaderboard</a>
        <a href="players/1" class="a">Players</a>
        <a href="feedmultiple" class="a">Feed</a>
    </div>
</div>

<style lang="scss">
    img {
        margin: 20px 0;
    }

    .billy-container {
        font-family: "Akira";
        display: flex;
        align-items: center;
        font-size: 24px;

        h1 {
            margin-left: 15px;
            font-size: 110px;
            margin-top: 64px;
        }
    }

    .container {
        display: flex;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        height: 100vh;
        margin-bottom: 50px;
    }

    .launchpad {
        display: grid;
        grid-template-columns: repeat(2, 300px);
        gap: 32px;
    }

    .a {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 300px;
        height: 300px;
        background-color: white;
        color: black;
        font-size: 16px;
        font-weight: bold;
        text-align: center;
        border: none;
        border-radius: 10px;
        cursor: pointer;
        transition: background-color 0.3;
        font-family: "Akira";
        text-decoration: none;

        box-shadow:
            14px 14px 0 -4px black,
            14px 14px 0 0 black;
        border: 3px solid black;
        border-radius: 20px;
    }

    .a:hover {
        background-color: #e5e5e5;
        // background-color: black;
        // color: white;
    }
</style>
