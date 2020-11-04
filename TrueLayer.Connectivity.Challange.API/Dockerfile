FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TrueLayer.Connectivity.Challange.API/TrueLayer.Connectivity.Challange.API.csproj", "TrueLayer.Connectivity.Challange.API/"]
RUN dotnet restore "TrueLayer.Connectivity.Challange.API/TrueLayer.Connectivity.Challange.API.csproj"
COPY . .
WORKDIR "/src/TrueLayer.Connectivity.Challange.API"
RUN dotnet build "TrueLayer.Connectivity.Challange.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrueLayer.Connectivity.Challange.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrueLayer.Connectivity.Challange.API.dll"]