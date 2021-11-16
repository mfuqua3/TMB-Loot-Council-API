FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Presentation/**/*.csproj ./lootcouncilapp/
COPY Domain/**/*.csproj ./lootcouncilapp/
COPY Engine/**/*.csproj ./lootcouncilapp/
COPY Service/**/*.csproj ./lootcouncilapp/
COPY Utility/**/*.csproj ./lootcouncilapp/
RUN dotnet restore

# copy everything else and build app
COPY . ./lootcouncilapp/
WORKDIR /source/lootcouncilapp
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "LootCouncil.Presentation.API.dll"]