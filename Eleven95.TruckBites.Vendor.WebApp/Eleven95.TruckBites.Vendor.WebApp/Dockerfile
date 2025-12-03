FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp.csproj", "Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp/"]
COPY ["Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp.Client/Eleven95.TruckBites.Vendor.WebApp.Client.csproj", "Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp.Client/"]
COPY ["Eleven95.TruckBites.Data/Eleven95.TruckBites.Data.csproj", "Eleven95.TruckBites.Data/"]
COPY ["Eleven95.TruckBites.Services/Eleven95.TruckBites.Services.csproj", "Eleven95.TruckBites.Services/"]
COPY ["Eleven95.TruckBites.Client.Shared/Eleven95.TruckBites.Client.Shared.csproj", "Eleven95.TruckBites.Client.Shared/"]
COPY ["Eleven95.TruckBites.Server.Shared/Eleven95.TruckBites.Server.Shared.csproj", "Eleven95.TruckBites.Server.Shared/"]
RUN dotnet restore "Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp.csproj"
COPY . .
WORKDIR "/src/Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp"
RUN dotnet build "./Eleven95.TruckBites.Vendor.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Eleven95.TruckBites.Vendor.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eleven95.TruckBites.Vendor.WebApp.dll"]
