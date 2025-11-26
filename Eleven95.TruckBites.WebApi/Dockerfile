FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Eleven95.TruckBites.WebApi/Eleven95.TruckBites.WebApi.csproj", "Eleven95.TruckBites.WebApi/"]
COPY ["Eleven95.TruckBites.Data/Eleven95.TruckBites.Data.csproj", "Eleven95.TruckBites.Data/"]
COPY ["Eleven95.TruckBites.Services/Eleven95.TruckBites.Services.csproj", "Eleven95.TruckBites.Services/"]
RUN dotnet restore "Eleven95.TruckBites.WebApi/Eleven95.TruckBites.WebApi.csproj"
COPY . .
WORKDIR "/src/Eleven95.TruckBites.WebApi"
RUN dotnet build "./Eleven95.TruckBites.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Eleven95.TruckBites.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eleven95.TruckBites.WebApi.dll"]
