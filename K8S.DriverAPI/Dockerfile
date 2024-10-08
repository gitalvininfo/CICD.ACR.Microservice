FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["K8S.DriverAPI.csproj", "K8S.DriverAPI/"]
COPY ["./K8S.Entities/K8S.Entities.csproj", "K8S.Entities/"]
RUN dotnet restore "K8S.DriverAPI.csproj"
## copy everything from local to docker image
COPY . .
WORKDIR "/src/K8S.DriverAPI"
RUN dotnet build "K8S.DriverAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

## whatever taken from this build, give different name as publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "K8S.DriverAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "K8S.DriverAPI.dll"]