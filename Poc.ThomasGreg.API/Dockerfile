# Etapa base para execução do app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5000

# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
 
COPY ["Poc.ThomasGreg.API/Poc.ThomasGreg.API.csproj", "Poc.ThomasGreg.API/"]
COPY ["Poc.ThomasGreg.Application/Poc.ThomasGreg.Application.csproj", "Poc.ThomasGreg.Application/"]
COPY ["Poc.ThomasGreg.Domain/Poc.ThomasGreg.Domain.csproj", "Poc.ThomasGreg.Domain/"]
COPY ["Poc.ThomasGreg.Infra/Poc.ThomasGreg.Infra.csproj", "Poc.ThomasGreg.Infra/"]

# Restaura os pacotes do .NET
RUN dotnet restore "Poc.ThomasGreg.API/Poc.ThomasGreg.API.csproj"

# Copia todos os arquivos do projeto para dentro do container
COPY . .

WORKDIR "/src/Poc.ThomasGreg.API"
RUN dotnet build "Poc.ThomasGreg.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Poc.ThomasGreg.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final: configuração do container de execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poc.ThomasGreg.API.dll"]
