# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FatSliceArchitecture.Api/FatSliceArchitecture.Api.csproj", "src/FatSliceArchitecture.Api/"]
COPY ["src/FatSliceArchitecture.Handlers/FatSliceArchitecture.Handlers.csproj", "src/FatSliceArchitecture.Handlers/"]
COPY ["src/FatSliceArchitecture.Infrastructure/FatSliceArchitecture.Infrastructure.csproj", "src/FatSliceArchitecture.Infrastructure/"]
COPY ["src/FatSliceArchitecture.Core/FatSliceArchitecture.Core.csproj", "src/FatSliceArchitecture.Core/"]
COPY ["src/FatSliceArchitecture.Domain/FatSliceArchitecture.Domain.csproj", "src/FatSliceArchitecture.Domain/"]
RUN dotnet restore "./src/FatSliceArchitecture.Api/FatSliceArchitecture.Api.csproj"
COPY . .
WORKDIR "/src/src/FatSliceArchitecture.Api"
RUN dotnet build "./FatSliceArchitecture.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FatSliceArchitecture.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FatSliceArchitecture.Api.dll"]