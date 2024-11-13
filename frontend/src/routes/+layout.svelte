<script lang="ts">
  import { page } from "$app/stores";
  import "../app.postcss";
  import Header from "./Header.svelte";
  import "./styles.css";
  import { onMount } from "svelte";
  // import {
  //   initAuth0,
  //   isAuthenticated,
  //   getUser,
  //   login,
  //   logout,
  //   handleRedirectCallback,
  // } from "../auth";
  import type { User } from "@auth0/auth0-spa-js";

  let user: User | null = null;
  let authenticated = false;

  // onMount(async () => {
  // await initAuth0();

  // const params = new URLSearchParams(window.location.search);
  // if (params.has("code") && params.has("state")) {
  //   // Handle the redirect and exchange the code for tokens
  //   await handleRedirectCallback();

  //   // Clear query parameters from the URL after handling the callback
  //   window.history.replaceState({}, document.title, window.location.pathname);
  // }

  //   authenticated = await isAuthenticated();
  //   if (authenticated) {
  //     user = (await getUser()) || null;
  //     console.log(user);
  //   }
  // });

  // In your layout or main entry point (e.g., src/routes/+layout.svelte)
  // Extract tenantId from subdomain or URL
  //   const host = window.location.host;
  //   const subdomain = host.split(".")[0]; // Assumes tenant is the first part of the domain

  //   // Store the tenant ID in localStorage (or another preferred method)
  //   if (subdomain) {
  //     window.localStorage.setItem("tenantId", subdomain);
  //   }

  // const handleLogin = async () => {
  //   await login();
  // };

  // const handleLogout = async () => {
  //   await logout();
  // };

  let isLaunchpad: boolean;
  $: isLaunchpad = $page.url.pathname.startsWith("/launchpad");
</script>

<div class="app">
  <!-- {#if !isLaunchpad} -->
  <Header />
  <!-- {/if} -->

  <!-- // {#if authenticated}
    // <p>Welcome {user?.name}!</p>
    //<button on:click={handleLogout}>Logout</button>
    //
  {:else}
    // <button on:click={handleLogin}>Login</button>
    //
  {/if} -->

  <main>
    <slot />
  </main>
</div>

<style>
  .app {
    display: flex;
    flex-direction: column;
    min-height: 100vh;
  }

  main {
    flex: 1;
    display: flex;
    flex-direction: column;
    padding: 1rem;
    width: 100%;
    max-width: 100rem;
    margin: 0 auto;
    box-sizing: border-box;
  }
</style>
