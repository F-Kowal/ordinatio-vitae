FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["OrdinatioVitae.Api/OrdinatioVitae.Api.csproj", "OrdinatioVitae.Api/"]
RUN dotnet restore "OrdinatioVitae.Api/OrdinatioVitae.Api.csproj"
COPY . .
WORKDIR "/src/OrdinatioVitae.Api"
RUN dotnet build "./OrdinatioVitae.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrdinatioVitae.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdinatioVitae.Api.dll"]
