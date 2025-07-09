# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["EQTAXTechnicalTestApp.sln", "."]
COPY ["EQTAXTechnicalTestApp.Api/EQTAXTechnicalTestApp.Api.csproj", "EQTAXTechnicalTestApp.Api/"]
COPY ["EQTAXTechnicalTestApp.Application/EQTAXTechnicalTestApp.Application.csproj", "EQTAXTechnicalTestApp.Application/"]
COPY ["EQTAXTechnicalTestApp.Domain/EQTAXTechnicalTestApp.Domain.csproj", "EQTAXTechnicalTestApp.Domain/"]
COPY ["EQTAXTechnicalTestApp.Infrastructure/EQTAXTechnicalTestApp.Infrastructure.csproj", "EQTAXTechnicalTestApp.Infrastructure/"]

RUN dotnet restore

COPY . .

WORKDIR "/src/EQTAXTechnicalTestApp.Api"
RUN dotnet build "EQTAXTechnicalTestApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EQTAXTechnicalTestApp.Api.csproj" -c Release -o /app/publish /p:PublishAot=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EQTAXTechnicalTestApp.Api.dll"]
