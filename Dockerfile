# Imagen base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen para construir la API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BlockSimApi/BlockSimApi.csproj", "BlockSimApi/"]
RUN dotnet restore "BlockSimApi/BlockSimApi.csproj"
COPY . .
WORKDIR "/src/BlockSimApi"
RUN dotnet build -c Release -o /app/build

# Publicar la API
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Imagen final con runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Ejecutar migraciones y luego iniciar la API
ENTRYPOINT ["dotnet", "BlockSimApi.dll"]
