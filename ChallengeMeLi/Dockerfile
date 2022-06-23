#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Buscar la imagne oficial del sdk de .net core 5
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5024

# Copiar los archivos del proyecto al contenedor
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ChallengeMeLi/ChallengeMeLi.csproj", "ChallengeMeLi/"]
COPY ["ChallengeMeLi.Persistence/ChallengeMeLi.Persistence.csproj", "ChallengeMeLi.Persistence/"]
COPY ["ChallengeMeLi.Application/ChallengeMeLi.Application.csproj", "ChallengeMeLi.Application/"]
COPY ["ChallengeMeLi.Shared/ChallengeMeLi.Shared.csproj", "ChallengeMeLi.Shared/"]
COPY ["ChallengeMeLi.Domain/ChallengeMeLi.Domain.csproj", "ChallengeMeLi.Domain/"]
RUN dotnet restore "ChallengeMeLi/ChallengeMeLi.csproj"
COPY . .
WORKDIR "/src/ChallengeMeLi"
RUN dotnet build "ChallengeMeLi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChallengeMeLi.csproj" -c Release -o /app/publish

# Crear imagen
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChallengeMeLi.dll"]