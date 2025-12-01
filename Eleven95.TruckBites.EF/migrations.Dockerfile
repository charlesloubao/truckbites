FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Eleven95.TruckBites.EF/Eleven95.TruckBites.EF.csproj", "Eleven95.TruckBites.EF/"]
COPY ["Eleven95.TruckBites.Data/Eleven95.TruckBites.Data.csproj", "Eleven95.TruckBites.Data/"]
RUN dotnet restore "Eleven95.TruckBites.EF/Eleven95.TruckBites.EF.csproj"
COPY . .
WORKDIR "/src/Eleven95.TruckBites.EF"
RUN dotnet build "./Eleven95.TruckBites.EF.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet tool install --global dotnet-ef --version 9.0.11
ENV PATH="$PATH:/root/.dotnet/tools"

# We pass a dummy connection string here just to prevent the build from crashing.
# The bundle DOES NOT bake this string in; it will use the environment variable at runtime.   
ENV ConnectionStrings__DefaultConnection="Server=dummy"
RUN dotnet ef migrations bundle -o /app/publish/efbundle --self-contained -r linux-x64

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./efbundle"]
