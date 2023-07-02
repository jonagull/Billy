// This file is used by pm2 to start the frontend and backend projects in parallel

module.exports = {
    apps: [
        {
            name: "Billy-FE",
            script: "npm",
            args: "run dev",
            cwd: "./frontend",
        },
        // This works only if youve built the backend project beforehand
        {
            name: "Billy-BE",
            script: "dotnet",
            args: "run --no-build",
            cwd: "./backend/Billy-BE",
        },
    ],
};
