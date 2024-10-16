# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY ["ApiStore/ApiStore.csproj", "ApiStore/"]
RUN dotnet restore "ApiStore/ApiStore.csproj"

# Copy everything else and build app
COPY . .
WORKDIR /source/ApiStore	
RUN dotnet publish -o /app

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ApiStore.dll"]