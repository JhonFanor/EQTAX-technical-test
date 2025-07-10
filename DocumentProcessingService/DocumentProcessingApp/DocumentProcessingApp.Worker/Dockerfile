FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore DocumentProcessingApp.Worker/DocumentProcessingApp.Worker.csproj

RUN dotnet publish DocumentProcessingApp.Worker/DocumentProcessingApp.Worker.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DocumentProcessingApp.Worker.dll"]
