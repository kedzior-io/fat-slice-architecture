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
COPY ["src/RichHandlerArchitecture.Api/RichHandlerArchitecture.Api.csproj", "src/RichHandlerArchitecture.Api/"]
COPY ["src/RichHandlerArchitecture.Handlers/RichHandlerArchitecture.Handlers.csproj", "src/RichHandlerArchitecture.Handlers/"]
COPY ["src/RichHandlerArchitecture.Infrastructure/RichHandlerArchitecture.Infrastructure.csproj", "src/RichHandlerArchitecture.Infrastructure/"]
COPY ["src/RichHandlerArchitecture.Core/RichHandlerArchitecture.Core.csproj", "src/RichHandlerArchitecture.Core/"]
COPY ["src/RichHandlerArchitecture.Domain/RichHandlerArchitecture.Domain.csproj", "src/RichHandlerArchitecture.Domain/"]
RUN dotnet restore "./src/RichHandlerArchitecture.Api/RichHandlerArchitecture.Api.csproj"
COPY . .
WORKDIR "/src/src/RichHandlerArchitecture.Api"
RUN dotnet build "./RichHandlerArchitecture.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RichHandlerArchitecture.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RichHandlerArchitecture.Api.dll"]