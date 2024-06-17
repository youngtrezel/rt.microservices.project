#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Marketing/Marketing.API/Marketing.API.csproj", "src/Services/Marketing/Marketing.API/"]
COPY ["Marketing.Domain/Marketing.Domain.csproj", "Marketing.Domain/"]
COPY ["src/Services/Marketing/Marketing.Infrastructure/Marketing.Infrastructure.csproj", "src/Services/Marketing/Marketing.Infrastructure/"]
COPY ["src/Services/Marketing/Marketing.Repository/Marketing.Repository.csproj", "src/Services/Marketing/Marketing.Repository/"]
RUN dotnet restore "./src/Services/Marketing/Marketing.API/Marketing.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Marketing/Marketing.API"
RUN dotnet build "./Marketing.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Marketing.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Marketing.API.dll"]