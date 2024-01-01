<script lang="ts">
    const changelog = [
        {
            title: "8. Nov 2023 - Changed flow for logging games",
            text: "Added combobox, changed layout for gamelogging",
        },
        {
            title: "11. Nov 2023 - Combobox",
            text: "Added multi combobox to chart page, now you can include players you want to compare in chart instead of excluding all the players you dont want to see",
        },
        {
            title: "20. Nov 2023 - Added opponents",
            text: "Added opponents to player profile, under metrics - so now you can see gamesplayed count against other players",
        },
        {
            title: "22. Nov 2023 - Pagination",
            text: "Added pagination to feed page, now you can see more than 10 games at a time",
        },
        {
            title: "26. Nov 2023 - Changed gamelogging flow, general style upgrade",
            text: "Changed flow for logging game, removed confirmation modal, added revert button that can be pressed within a time frame after a game is logged ",
        },
        {
            title: "31. Desember 2023 - Added opponent chart",
            text: "Added charts for opponents, now you can see how you perform against other players",
        },
    ];
</script>

<h1>Changelog</h1>

<hr style="margin: 20px 0" />

<article class="container mx-auto px-4 sm:px-6 lg:px-8">
    <div
        class="prose mt-10 mb-10 sm:prose-sm md:prose-md lg:prose-lg xl:prose-xl mx-auto table-responsive"
    >
        {#each changelog.reverse() as cl}
            <div style="margin-top: 10px">
                <h2>
                    <strong>{cl.title}</strong>
                </h2>
                <p>
                    {cl.text}
                </p>
                <hr style="margin-top: 10px" />
            </div>
        {/each}
    </div>
</article>
