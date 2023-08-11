# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the source code to the container
COPY . .

# Build the application
RUN dotnet restore
RUN dotnet build -c Release --no-restore

# Publish the application
RUN dotnet publish -c Release -o out --no-restore

# Use the official ASP.NET Core runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Copy the SQLite database file
COPY data.db .

# Expose port 80
EXPOSE 80 

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Billy-BE.dll"]


# docker run -d -p 80:80 -v /path/on/host/data.db:/app/data.db my-dotnet-app
