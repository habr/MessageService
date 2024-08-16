# Используем базовый образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем проект и устанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем оставшуюся часть проекта и выполняем сборку
COPY . ./
RUN dotnet publish -c Release -o out

# Используем базовый образ .NET Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Указываем порт
EXPOSE 7223
ENTRYPOINT ["dotnet", "MessageService.dll"]
