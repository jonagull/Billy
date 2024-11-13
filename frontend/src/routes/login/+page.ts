// import { baseUrl } from "$lib/constants";
// import { Auth0Client } from "@auth0/auth0-spa-js";
// import type { PageLoad } from "./$types";
// import { handleRedirectCallback, initAuth0 } from "../../auth";
// // plet auth0: Auth0Client | undefined;

// // export const load = (async () => {
// export const load = (async ({ params, url }) => {
//   //   let auth0: Auth0Client | undefined;
//   let code = url.searchParams.get("code");

//   if (code) {
//     await handleRedirectCallback();
//     // await auth0.handleRedirectCallback();

//     console.log("CODE", code);
//     // let accessToken = await auth0.getTokenSilently();

//     // console.log("ASKJDHASLKJDHASLKJHDASLKJHDASLKJHD", accessToken);

//     // if (accessToken) {
//     //   sessionStorage.setItem("access_token", accessToken);
//     // }
//   }
// }) satisfies PageLoad;
