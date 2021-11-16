FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Presentation/LootCouncil.Presentation.API/LootCouncil.Presentation.API.csproj", "LootCouncil.Presentation.API/"]
RUN dotnet restore "LootCouncil.Presentation.API/LootCouncil.Presentation.API.csproj"
COPY . .
WORKDIR "/src/LootCouncil.Presentation.API"
RUN dotnet build "LootCouncil.Presentation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LootCouncil.Presentation.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LootCouncil.Presentation.API.dll"]
