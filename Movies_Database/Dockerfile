# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Movies_Database/Movies_Database.csproj", "Movies_Database/"]
RUN dotnet restore "Movies_Database/Movies_Database.csproj"
COPY . .
WORKDIR "/src/Movies_Database"

RUN dotnet tool install --global dotnet-ef --version 6.0.0
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet build "Movies_Database.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movies_Database.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY https/aspnetapp.pfx /https/aspnetapp.pfx

ENTRYPOINT ["dotnet", "Movies_Database.dll"]