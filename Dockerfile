﻿FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Presentation/LootCouncil.Presentation.API/*.csproj ./Presentation/LootCouncil.Presentation.API/
COPY Domain/LootCouncil.Domain.Common/*.csproj ./Domain/LootCouncil.Domain.Common/
COPY Domain/LootCouncil.Domain.Data/*.csproj ./Domain/LootCouncil.Domain.Data/
COPY Domain/LootCouncil.Domain.DataContracts/*.csproj ./Domain/LootCouncil.Domain.DataContracts/
COPY Domain/LootCouncil.Domain.Entities/*.csproj ./Domain/LootCouncil.Domain.Entities/
COPY Engine/LootCouncil.Engine.Common/*.csproj ./Engine/LootCouncil.Engine.Common/
COPY Engine/LootCouncil.Engine/*.csproj ./Engine/LootCouncil.Engine/
COPY Service/LootCouncil.Service.Common/*.csproj ./Service/LootCouncil.Service.Common/
COPY Service/LootCouncil.Service/*.csproj ./Service/LootCouncil.Service/
COPY Utility/LootCouncil.Utility/*.csproj ./Utility/LootCouncil.Utility/
RUN dotnet restore

# copy everything else and build app
COPY . .
RUN dotnet build
FROM build AS publish
# COPY Presentation/LootCouncil.Presentation.API ./Presentation/LootCouncil.Presentation.API/
# COPY Domain/LootCouncil.Domain.Common/ ./Domain/LootCouncil.Domain.Common/
# COPY Domain/LootCouncil.Domain.Data/ ./Domain/LootCouncil.Domain.Data/
# COPY Domain/LootCouncil.Domain.DataContracts/ ./Domain/LootCouncil.Domain.DataContracts/
# COPY Domain/LootCouncil.Domain.Entities/ ./Domain/LootCouncil.Domain.Entities/
# COPY Engine/LootCouncil.Engine.Common/ ./Engine/LootCouncil.Engine.Common/
# COPY Engine/LootCouncil.Engine/ ./Engine/LootCouncil.Engine/
# COPY Service/LootCouncil.Service.Common/ ./Service/LootCouncil.Service.Common/
# COPY Service/LootCouncil.Service/ ./Service/LootCouncil.Service/
# COPY Utility/LootCouncil.Utility/ ./Utility/LootCouncil.Utility/
WORKDIR /app/Presentation/LootCouncil.Presentation.API/
RUN dotnet publish -c Release -o out --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=publish /app/Presentation/LootCouncil.Presentation.API/out ./
EXPOSE 80
EXPOSE 443
ENV ConnectionStrings:DefaultConnection=Host=guildview-dev.c6rdqolhzhx8.us-east-1.rds.amazonaws.com;Username=postgres;Password=%xhvmUyXY^U3wx#7;Database=LootCouncilDev;
ENV DiscordAuthentication:ClientSecret=Tt_4-LkTEmv0u-efuZYKuGdGMnQ1iYry
ENV JwtBearer:Secret=OFo0dkNnRUxNYXZ1Vkw5bnNMRWJEdHM3U0dEdmZYOGk4d3A2YWpFNEpjWTZTR0p3UTl6Yg==
ENTRYPOINT ["dotnet", "LootCouncil.Presentation.API.dll"]