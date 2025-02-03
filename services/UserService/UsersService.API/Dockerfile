# Используем официальный образ .NET SDK для создания приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Используем образ для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Убедитесь, что путь верный относительно контекста, который передается при сборке
COPY ["UsersService.API/UsersService.API.csproj", "UsersService.API/"]
RUN dotnet restore "UsersService.API/UsersService.API.csproj"

COPY . . 
WORKDIR "/src/UsersService.API"
RUN dotnet build "UsersService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UsersService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "UsersService.API.dll"]
