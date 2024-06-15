#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Commercial/Commercial.API/Commercial.API.csproj", "src/Services/Commercial/Commercial.API/"]
COPY ["src/Services/Commercial/Commercial.Domain/Commercial.Domain.csproj", "src/Services/Commercial/Commercial.Domain/"]
COPY ["src/Services/Commercial/Commercial.Infrastructure/Commercial.Infrastructure.csproj", "src/Services/Commercial/Commercial.Infrastructure/"]
COPY ["src/Services/Commercial/Commercial.Repository/Commercial.Repository.csproj", "src/Services/Commercial/Commercial.Repository/"]
RUN dotnet restore "./src/Services/Commercial/Commercial.API/Commercial.API.csproj"
COPY . .
RUN dotnet tool install --version 8.0.6 --global dotnet-ef

WORKDIR "/src/src/Services/Commercial/Commercial.API"
RUN dotnet build "./Commercial.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN /root/.dotnet/tools/dotnet-ef migrations add InitMigration --project Commercial.Infrastructure.csproj -v
RUN /root/.dotnet/tools/dotnet-ef database update

WORKDIR "/src/src/Services/Commercial/Commercial.API"
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Commercial.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Commercial.API.dll"]