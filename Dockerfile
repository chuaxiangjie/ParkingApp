#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ParkingApp/ParkingApp.csproj", "ParkingApp/"]
COPY ["CarParkService/CarParkService.csproj", "CarParkService/"]
COPY ["Libraries/Libraries.csproj", "Libraries/"]
RUN dotnet restore "ParkingApp/ParkingApp.csproj"
COPY . .
WORKDIR "/src/ParkingApp"
RUN dotnet build "ParkingApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParkingApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParkingApp.dll"]