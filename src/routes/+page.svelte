<!-- App.svelte -->
<script>
    import Feed from "$lib/images/components/Feed.svelte";
</script>

<main>
    <div class="welcome">
        <img
            src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/71/Cegal-black-logo.svg/2560px-Cegal-black-logo.svg.png"
            alt=""
            width="300px"
        />
        <h2>Welcome to Billy</h2>
        <p>
            Are you ready to showcase your skills and compete against the best?
            Billy empowers you to keep track your progress and compare you
            internally to you colleagues and playing partners!
        </p>
        <a href="/players">
            <button class="cta-button">Get Started</button>
        </a>
    </div>
</main>

<Feed />

<style>
    main {
        display: flex;
        align-items: center;
        justify-content: center;
        height: calc(100vh - 80px);
    }

    .welcome {
        text-align: center;
    }

    h2 {
        font-size: 32px;
        margin-bottom: 20px;
    }

    p {
        font-size: 18px;
        margin-bottom: 30px;
    }

    .cta-button {
        display: inline-block;
        padding: 12px 24px;
        background-color: #333;
        color: #fff;
        text-decoration: none;
        font-size: 18px;
        border-radius: 4px;
        transition: background-color 0.3s ease;
        cursor: pointer;
    }

    .cta-button:hover {
        background-color: #555;
    }
</style>
