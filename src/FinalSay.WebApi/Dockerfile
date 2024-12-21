FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FinalSay.WebApi/FinalSay.WebApi.csproj", "src/FinalSay.WebApi/"]
RUN dotnet restore "src/FinalSay.WebApi/FinalSay.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FinalSay.WebApi"
RUN dotnet build "FinalSay.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinalSay.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./src/FinalSay.WebApi/appsettings.json /app/appsettings.json
ENTRYPOINT ["dotnet", "FinalSay.WebApi.dll"]
