# MessageService

## ��������

MessageService � ��� ������� ���-������ ��� ������ �����������, ��������� �� ���� �����������: Web-������ �� ASP.NET Core, ���� ������ PostgreSQL � ��� ������� ��� �������� � ��������� ���������.

### �����������

- **Web-������:** ASP.NET Core, ��������� REST API � WebSocket.
- **���� ������:** PostgreSQL.
- **�������:**
  - ������ ������ ���������� ��������� ����� REST API.
  - ������ ������ �������� ��������� � �������� ������� ����� WebSocket.
  - ������ ������ ���������� ��������� �� ��������� 10 �����.

### ���� ����������

- C# / ASP.NET Core
- PostgreSQL
- Docker / Docker-compose
- HTML / JavaScript

## ��� ���������

### ��������� ������ � Docker

1. ���������, ��� Docker ���������� � ��������.
2. ����������� �����������:

   ```bash
   git clone https://github.com/your-username/MessageService.git
   cd MessageService

3. ��������� � ��������� ����������:

	```bash
	docker-compose up --build


4. ���������� ����� �������� �� ������: https://localhost:7223.

### �������
�������� ���������: �������� ���� client-sender.html � ��������.
�������� ��������� � �������� �������: �������� ���� client-realtime.html.
�������� ��������� �� ��������� 10 �����: �������� ���� client-last-10.html.
API ������������
API-������������ �������� �� ������: https://localhost:7223/swagger/index.html

### �������������� ����������
������ ������������ CORS ��� �������������� � ���������.
Docker-compose �������� ��� ������������� ���� ������ PostgreSQL � ASP.NET Core ����������.