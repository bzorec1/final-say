FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FinalSay.Worker.csproj", "FinalSay.Worker/"]
RUN dotnet restore "FinalSay.Worker/FinalSay.Worker.csproj"

COPY . FinalSay.Worker/.
RUN dotnet publish -c Release -o /app FinalSay.Worker/FinalSay.Worker.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS publish
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "FinalSay.Worker.dll"]