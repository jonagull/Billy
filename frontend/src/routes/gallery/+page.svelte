<script lang="ts">
    import { youtubeVideos } from "$lib/data/youtubeVideos";

    let randomIndex = Math.floor(Math.random() * youtubeVideos.length);
</script>

<h1 class="mb-5">Gallery</h1>

<h3 class="flex justify-center">{youtubeVideos[randomIndex].title}</h3>

<div class="flex justify.-center">
    <iframe
        width="1200"
        height="600"
        src={youtubeVideos[randomIndex].link}
        title="YouTube video player"
        frameborder="0"
        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
        allowfullscreen
    />
</div>
