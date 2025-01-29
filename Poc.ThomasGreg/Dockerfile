FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
 
COPY ["Poc.ThomasGreg/Poc.ThomasGreg.MVC.csproj", "Poc.ThomasGreg/"]
 
RUN dotnet restore "Poc.ThomasGreg/Poc.ThomasGreg.MVC.csproj"

COPY . .

WORKDIR "/src/Poc.ThomasGreg"
RUN dotnet build "Poc.ThomasGreg.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Poc.ThomasGreg.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poc.ThomasGreg.MVC.dll"]
