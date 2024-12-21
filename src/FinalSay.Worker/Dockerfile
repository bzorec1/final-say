FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["FinalSay.Worker.csproj", "FinalSay.Worker/"]
RUN dotnet restore "FinalSay.Worker/FinalSay.Worker.csproj"

COPY . FinalSay.Worker/.
RUN dotnet publish -c Release -o /app FinalSay.Worker/FinalSay.Worker.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS publish
WORKDIR /app
COPY --from=build /app ./
COPY ./src/FinalSay.Worker/appsettings.json /app/appsettings.json
ENTRYPOINT ["dotnet", "FinalSay.Worker.dll"]