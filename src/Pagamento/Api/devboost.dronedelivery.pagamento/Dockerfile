#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.11 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build
WORKDIR /src
COPY ["Pagamento/Api/devboost.dronedelivery.pagamento/devboost.dronedelivery.pagamento.Api.csproj", "Pagamento/Api/devboost.dronedelivery.pagamento/"]
COPY ["Infra/Data/devboost.dronedelivery.Infra.Data.csproj", "Infra/Data/"]
COPY ["Infra/Security/Domain/devboost.dronedelivery.security.domain/devboost.dronedelivery.security.domain.csproj", "Infra/Security/Domain/devboost.dronedelivery.security.domain/"]
COPY ["Delivery/Domain/devboost.dronedelivery.domain/devboost.dronedelivery.domain.csproj", "Delivery/Domain/devboost.dronedelivery.domain/"]
COPY ["Core/Domain/devboost.dronedelivery.core.domain.csproj", "Core/Domain/"]
COPY ["Delivery/Services/devboost.dronedelivery.Services.csproj", "Delivery/Services/"]
COPY ["Core/Services/devboost.dronedelivery.core.services/devboost.dronedelivery.core.services.csproj", "Core/Services/devboost.dronedelivery.core.services/"]
COPY ["Pagamento/Services/devboost.microservices.pagamento.services/devboost.dronedelivery.pagamento.services.csproj", "Pagamento/Services/devboost.microservices.pagamento.services/"]
COPY ["Pagamento/Infra/devboost.dronedelivery.pagamento.EF/devboost.dronedelivery.pagamento.EF.csproj", "Pagamento/Infra/devboost.dronedelivery.pagamento.EF/"]
COPY ["Pagamento/Domain/devboost.dronedelivery.pagamento.domain/devboost.dronedelivery.pagamento.domain.csproj", "Pagamento/Domain/devboost.dronedelivery.pagamento.domain/"]
RUN dotnet restore "Pagamento/Api/devboost.dronedelivery.pagamento/devboost.dronedelivery.pagamento.Api.csproj"
COPY . .
WORKDIR "/src/Pagamento/Api/devboost.dronedelivery.pagamento"
RUN dotnet build "devboost.dronedelivery.pagamento.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "devboost.dronedelivery.pagamento.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "devboost.dronedelivery.pagamento.Api.dll"]