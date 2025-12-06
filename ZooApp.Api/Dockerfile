FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# KOPIOWANIE PLIKÓW PROJEKTÓW
# Kopiujemy każdy csproj osobno, aby wykorzystać cache warstw Dockera.
# Jeśli zmienisz kod w Domain, a nie ruszysz csproj, Docker nie będzie musiał ponownie robić 'dotnet restore'.
COPY ["ZooApp.Api/ZooApp.Api.csproj", "ZooApp.Api/"]
COPY ["ZooApp.Application/ZooApp.Application.csproj", "ZooApp.Application/"]
COPY ["ZooApp.Domain/ZooApp.Domain.csproj", "ZooApp.Domain/"]
COPY ["ZooApp.Infrastructure/ZooApp.Infrastructure.csproj", "ZooApp.Infrastructure/"]

# Restore zależności dla całego API
RUN dotnet restore "ZooApp.Api/ZooApp.Api.csproj"

# Kopiowanie reszty kodu źródłowego (wszystkie foldery)
COPY . .

# Instalacja narzędzia
RUN dotnet tool install --global dotnet-ef

# WAŻNE: Dodanie ścieżki do zmiennych środowiskowych
ENV PATH="${PATH}:/root/.dotnet/tools"

# Ustawienie katalogu roboczego na API
WORKDIR "/src/ZooApp.Api"

# Ten etap służy do developmentu (zostanie użyty w docker-compose)
# Domyślnie budujemy, ale w dev nadpiszemy to przez 'dotnet watch'

FROM build AS publish
RUN dotnet publish "ZooApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZooApp.Api.dll"]