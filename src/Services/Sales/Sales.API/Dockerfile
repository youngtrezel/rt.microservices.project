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
COPY ["src/Services/Sales/Sales.API/Sales.API.csproj", "src/Services/Sales/Sales.API/"]
COPY ["src/Services/Sales/Sales.Domain/Sales.Domain.csproj", "src/Services/Sales/Sales.Domain/"]
COPY ["src/Services/Sales/Sales.Infrastructure/Sales.Infrastructure.csproj", "src/Services/Sales/Sales.Infrastructure/"]
COPY ["src/Services/Sales/Sales.Repository/Sales.Repository.csproj", "src/Services/Sales/Sales.Repository/"]
RUN dotnet restore "./src/Services/Sales/Sales.API/Sales.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Sales/Sales.API"
RUN dotnet build "./Sales.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sales.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sales.API.dll"]