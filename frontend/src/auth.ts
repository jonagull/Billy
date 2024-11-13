import { SvelteKitAuth } from "@auth/sveltekit";

export const { handle } = SvelteKitAuth({
  providers: [],
});

// import { Auth0Client, User, createAuth0Client } from "@auth0/auth0-spa-js";

// export let auth0: Auth0Client | undefined;
// let accessToken: string | null = null;

// export async function initAuth0(): Promise<void> {
//   console.log("INIINTIENIENTIENIE");
//   auth0 = await createAuth0Client({
//     // domain: "localhost:5173",
//     domain: "dev-moynkfr6lyd3iyf7.eu.auth0.com",
//     clientId: "hvl7epYMIlipWqte71oHmfyj3pvlQcWy",
//   });
// }

// // clientSecret: wY9A6gNK6Ub1Cswlx1LweEof3LuhL52RTrb4ztQCsc2tladjnkBOkqDl85YOKSQr

// // export async function login(): Promise<void> {
// //   if (auth0) {
// //     await auth0.loginWithRedirect();
// //   }
// // }

// export async function login(): Promise<void> {
//   if (auth0) {
//     await auth0.loginWithRedirect({
//       authorizationParams: {
//         // redirect_uri: window.location.origin,
//         redirect_uri: "http://localhost:5173/login",
//       },
//     });
//   }
// }

// export async function handleRedirectCallback(): Promise<void> {
//   console.log("CALLED");

//   if (!auth0) {
//     await initAuth0();
//     return;
//   }

//   console.log(auth0);
//   if (auth0) {
//     await auth0.handleRedirectCallback();

//     console.log("accestoken", accessToken);

//     // Get the access token after handling the redirect
//     accessToken = await auth0.getTokenSilently();

//     // Optional: store the token in session storage or local storage
//     sessionStorage.setItem("access_token", accessToken);
//   }
// }

// export async function logout(): Promise<void> {
//   if (auth0) {
//     auth0.logout({
//       openUrl(url) {
//         window.location.replace(url);
//       },
//       //   openUrl: window.location.origin,
//     });
//   }
// }

// export async function isAuthenticated(): Promise<boolean> {
//   if (auth0) {
//     return await auth0.isAuthenticated();
//   }
//   return false;
// }

// export async function getUser(): Promise<User | undefined> {
//   if (auth0) {
//     return await auth0.getUser();
//   }
// }

// export async function getToken(): Promise<string | undefined> {
//   if (auth0) {
//     return await auth0.getTokenSilently();
//   }
// }
