# Use .NET 8 SDK to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Restore dependencies
RUN dotnet restore API_GHSMS/API_GHSMS.csproj

# Build and publish
RUN dotnet publish API_GHSMS/API_GHSMS.csproj -c Release -o /app/publish

# Use .NET 8 runtime for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Entry point
ENTRYPOINT ["dotnet", "API_GHSMS.dll"]
