#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SoccerPlus.Api/SoccerPlus.Api.csproj", "src/SoccerPlus.Api/"]
COPY ["src/SoccerPlus.Infra.CrossCutting.IoC/SoccerPlus.Infra.CrossCutting.IoC.csproj", "src/SoccerPlus.Infra.CrossCutting.IoC/"]
COPY ["src/SoccerPlus.Application/SoccerPlus.Application.csproj", "src/SoccerPlus.Application/"]
COPY ["src/SoccerPlus.Domain/SoccerPlus.Domain.csproj", "src/SoccerPlus.Domain/"]
COPY ["src/SoccerPlus.Infra.Http/SoccerPlus.Infra.Http.csproj", "src/SoccerPlus.Infra.Http/"]
COPY ["src/SoccerPlus.Infra.Utils/SoccerPlus.Infra.Utils.csproj", "src/SoccerPlus.Infra.Utils/"]
COPY ["src/SoccerPlus.Infra.Data/SoccerPlus.Infra.Data.csproj", "src/SoccerPlus.Infra.Data/"]
RUN dotnet restore "./src/SoccerPlus.Api/SoccerPlus.Api.csproj"
COPY . .
WORKDIR "/src/src/SoccerPlus.Api"
RUN dotnet build "./SoccerPlus.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SoccerPlus.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoccerPlus.Api.dll"]