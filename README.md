# MessageService

## Описание

MessageService — это простой веб-сервис для обмена сообщениями, состоящий из трех компонентов: Web-сервер на ASP.NET Core, база данных PostgreSQL и три клиента для отправки и получения сообщений.

### Архитектура

- **Web-сервер:** ASP.NET Core, поддержка REST API и WebSocket.
- **База данных:** PostgreSQL.
- **Клиенты:**
  - Первый клиент отправляет сообщения через REST API.
  - Второй клиент получает сообщения в реальном времени через WebSocket.
  - Третий клиент отображает сообщения за последние 10 минут.

### Стек технологий

- C# / ASP.NET Core
- PostgreSQL
- Docker / Docker-compose
- HTML / JavaScript

## Как запустить

### Локальный запуск с Docker

1. Убедитесь, что Docker установлен и работает.
2. Склонируйте репозиторий:

   ```bash
   git clone https://github.com/your-username/MessageService.git
   cd MessageService

3. Постройте и запустите контейнеры:

	```bash
	docker-compose up --build


4. Приложение будет доступно по адресу: https://localhost:7223.

### Клиенты
Отправка сообщений: Откройте файл client-sender.html в браузере.
Просмотр сообщений в реальном времени: Откройте файл client-realtime.html.
Просмотр сообщений за последние 10 минут: Откройте файл client-last-10.html.
API Документация
API-документация доступна по адресу: https://localhost:7223/swagger/index.html

### Дополнительная информация
Проект поддерживает CORS для взаимодействия с клиентами.
Docker-compose настроен для развертывания базы данных PostgreSQL и ASP.NET Core приложения.