# Скачиваем образ(image) docker с официального сайта;
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Назначение рабочей директории, откуда будет работать образ(image);
WORKDIR /app

# Добовляем все файлы проекта в docker;
COPY . /app

# Создание связей в проекте;
RUN dotnet restore /app/Backend_asp.net.csproj

# Запуск проета в контейнере;
RUN dotnet publish -c Release -o out

# Запуск проекта в контейнере;
CMD ["dotnet", "out/DockerProjectBackend.dll"]