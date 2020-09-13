FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.11 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build
WORKDIR /src
COPY ["Delivery/Api/devboost.dronedelivery.Api.csproj", "Delivery/Api/"]
COPY ["Infra/Security/Domain/devboost.dronedelivery.security.domain/devboost.dronedelivery.security.domain.csproj", "Infra/Security/Domain/devboost.dronedelivery.security.domain/"]
COPY ["Infra/Data/devboost.dronedelivery.Infra.Data.csproj", "Infra/Data/"]
COPY ["Delivery/Domain/devboost.dronedelivery.domain/devboost.dronedelivery.domain.csproj", "Delivery/Domain/devboost.dronedelivery.domain/"]
COPY ["Core/Domain/devboost.dronedelivery.core.domain.csproj", "Core/Domain/"]
COPY ["Delivery/Services/devboost.dronedelivery.Services.csproj", "Delivery/Services/"]
COPY ["Core/Services/devboost.dronedelivery.core.services/devboost.dronedelivery.core.services.csproj", "Core/Services/devboost.dronedelivery.core.services/"]
COPY ["Infra/Security/Services/devboost.dronedelivery.security.service.csproj", "Infra/Security/Services/"]
RUN dotnet restore "Delivery/Api/devboost.dronedelivery.Api.csproj"
COPY . .
WORKDIR "/src/Delivery/Api"
RUN dotnet build "devboost.dronedelivery.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "devboost.dronedelivery.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "devboost.dronedelivery.Api.dll"]