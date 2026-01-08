FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ZooApp.Api/ZooApp.Api.csproj", "ZooApp.Api/"]
COPY ["ZooApp.Application/ZooApp.Application.csproj", "ZooApp.Application/"]
COPY ["ZooApp.Domain/ZooApp.Domain.csproj", "ZooApp.Domain/"]
COPY ["ZooApp.Infrastructure/ZooApp.Infrastructure.csproj", "ZooApp.Infrastructure/"]

RUN dotnet restore "ZooApp.Api/ZooApp.Api.csproj"

COPY . .

RUN dotnet tool install --global dotnet-ef

ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR "/src/ZooApp.Api"


FROM build AS publish
RUN dotnet publish "ZooApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZooApp.Api.dll"]