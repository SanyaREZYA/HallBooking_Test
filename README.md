# HallBooking_Test

ASP.NET Core Web API на .NET 8 для керування бронюванням конференц-залів. Проєкт використовує Entity Framework Core з PostgreSQL, шарову архітектуру `Controller -> Service -> Repository -> DbContext -> PostgreSQL` та Swagger для перевірки API.

## Основні можливості

- створення, редагування та видалення конференц-залів;
- збереження місткості, базової погодинної вартості та активності залу;
- керування додатковими послугами залу та їх цінами;
- створення, редагування та видалення клієнтів;
- створення та керування бронюваннями;
- вибір додаткових послуг під час бронювання;
- перевірка доступності залу за датою, часом і місткістю;
- автоматичний розрахунок вартості бронювання;
- керування статусами бронювання;
- звіти та базова аналітика по залах, клієнтах і популярності опцій.

## Технології

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- Npgsql.EntityFrameworkCore.PostgreSQL
- Swagger / Swashbuckle.AspNetCore
- PostgreSQL

## Структура Проєкту

- `Controllers/` - HTTP API endpoints для залів, опцій, клієнтів, бронювань і звітів.
- `Services/` - бізнес-логіка, валідація правил і розрахунок ціни.
- `Repositories/` - доступ до даних через `AppDbContext`.
- `Interfaces/` - контракти для сервісів і репозиторіїв.
- `Models/` - сутності доменної моделі та enum `BookingStatus`.
- `DTOs/` - об'єкти для створення та оновлення даних, а також DTO для звітів.
- `DataAccess/AppDbContext.cs` - конфігурація EF Core, зв'язки, precision, seed-дані.
- `Migrations/` - міграції бази даних.
- `appsettings.json` - конфігурація підключення до PostgreSQL.
- `Properties/launchSettings.json` - профілі запуску локально.

## Архітектура

Контролери приймають HTTP-запити і передають їх у сервіси. Сервіси містять бізнес-правила, перевірку даних і виклик додаткових сервісів, наприклад розрахунок вартості. Репозиторії інкапсулюють запити до `AppDbContext`, а він працює з PostgreSQL через EF Core.

У поточній реалізації:

- `Controller` повертає сутності або DTO з HTTP-відповідей;
- `Service` перевіряє умови бронювання, існування залу та опцій;
- `Repository` виконує запити до БД;
- `DbContext` описує схему, зв'язки, типи колонок і seed-дані;
- PostgreSQL зберігає дані.

## Вимоги

- .NET SDK 8.0;
- PostgreSQL 14+ або сумісна версія;
- доступ до бази даних, вказаної у connection string;
- `dotnet ef` для роботи з міграціями.

## Встановлення Та Запуск

1. Клонуйте репозиторій.
2. Налаштуйте PostgreSQL і створіть базу даних `hallbooking` або змініть connection string.
3. Встановіть залежності та застосуйте міграції.

```bash
dotnet restore
dotnet ef database update
```

4. Запустіть API.

```bash
dotnet run
```

Для запуску з локальним профілем можна використати:

```bash
dotnet run --launch-profile http
```

або

```bash
dotnet run --launch-profile https
```

## Налаштування PostgreSQL

Підключення задано в `appsettings.json` і `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=hallbooking;Username=postgres;Password=postgres"
}
```

За потреби змініть:

- `Host` - адреса сервера PostgreSQL;
- `Port` - порт;
- `Database` - назва бази;
- `Username` і `Password` - облікові дані.

Після зміни connection string повторно виконайте:

```bash
dotnet ef database update
```

## Міграції

У проєкті вже є міграції:

- `20260902095115_InitialCreate`
- `20260902133849_AddCustomerCreatedAt`

Для створення нової міграції:

```bash
dotnet ef migrations add <MigrationName>
```

Для застосування змін до бази:

```bash
dotnet ef database update
```

Якщо `dotnet ef` не встановлений, додайте його глобально:

```bash
dotnet tool install --global dotnet-ef
```

## Swagger

Swagger увімкнений лише в `Development`:

- `https://localhost:7234/swagger`
- `http://localhost:5014/swagger`

Профілі запуску описані в `Properties/launchSettings.json`.

## Основні API Endpoints

### Зали

- `GET /api/Hall/halls` - отримати всі зали.
- `GET /api/Hall/{id}` - отримати зал за ID.
- `POST /api/Hall` - створити зал.
- `PUT /api/Hall/{id}` - оновити зал.
- `DELETE /api/Hall/{id}` - видалити зал.
- `GET /api/Hall/available?startTime=...&endTime=...&capacity=...` - знайти доступні зали.

Приклад `POST /api/Hall`:

```json
{
  "name": "Зал D",
  "capacity": 40,
  "hourlyRate": 2200,
  "isActive": true
}
```

### Додаткові Опції Залу

- `GET /api/HallOption/hall-options` - отримати всі опції.
- `GET /api/HallOption/{id}` - отримати опцію за ID.
- `POST /api/HallOption` - створити опцію.
- `PUT /api/HallOption/{id}` - оновити опцію.
- `DELETE /api/HallOption/{id}` - видалити опцію.

Приклад `POST /api/HallOption`:

```json
{
  "name": "Кава-брейк",
  "price": 800,
  "isActive": true
}
```

### Клієнти

- `GET /api/Customer/customers` - отримати всіх клієнтів.
- `GET /api/Customer/{id}` - отримати клієнта за ID.
- `POST /api/Customer` - створити клієнта.
- `PUT /api/Customer/{id}` - оновити клієнта.
- `DELETE /api/Customer/{id}` - видалити клієнта.

Приклад `POST /api/Customer`:

```json
{
  "firstName": "Іван",
  "lastName": "Петренко",
  "phoneNumber": "+380501112233",
  "email": "ivan@example.com"
}
```

### Бронювання

- `GET /api/Booking/bookings` - отримати всі бронювання.
- `GET /api/Booking/{id}` - отримати бронювання за ID.
- `GET /api/Booking/customer/{customerId}?status=Confirmed` - бронювання клієнта з необов'язковим фільтром за статусом.
- `GET /api/Booking/hall/{hallId}?status=Cancelled` - бронювання залу з необов'язковим фільтром за статусом.
- `POST /api/Booking` - створити бронювання.
- `PUT /api/Booking/{id}` - оновити бронювання.
- `PATCH /api/Booking/{id}/status?status=Completed` - змінити статус бронювання.
- `DELETE /api/Booking/{id}` - видалити бронювання.

Підтримувані значення `BookingStatus`:

- `Confirmed = 1`
- `Cancelled = 2`
- `Completed = 3`

Приклад `POST /api/Booking`:

```json
{
  "hallId": 1,
  "customerId": 2,
  "startTime": "2026-09-10T10:00:00",
  "endTime": "2026-09-10T13:00:00",
  "status": 1,
  "hallOptionIds": [1, 2]
}
```

Приклад `PUT /api/Booking/{id}`:

```json
{
  "hallId": 2,
  "startTime": "2026-09-10T14:00:00",
  "endTime": "2026-09-10T16:00:00",
  "hallOptionIds": [3]
}
```

## Звіти Та Аналітика

- `GET /api/Report/customers?from=...&to=...` - звіт по клієнтах.
- `GET /api/Report/halls?from=...&to=...` - звіт по залах.
- `GET /api/Report/hall-options?from=...&to=...` - звіт по популярності опцій.

Що повертають звіти:

- звіт по клієнтах містить загальну кількість клієнтів, кількість нових клієнтів і кількість клієнтів, які мали бронювання в періоді;
- звіт по залах містить `HallId`, назву залу, кількість бронювань, кількість годин і дохід;
- звіт по опціях містить назву опції та кількість використань.

## Особливості Розрахунку Та Часу

- вартість бронювання рахується погодинно;
- тривалість бронювання має бути цілим числом годин, інакше сервіс кидає помилку;
- додаткові послуги додаються окремо до підсумкової вартості;
- тариф по годинах у поточній реалізації:
  - `06:00-09:00` - знижка 10%;
  - `09:00-18:00` - стандартна ціна;
  - `12:00-14:00` - націнка 15%;
  - `18:00-23:00` - знижка 20%;
- `StartTime` і `EndTime` зберігаються в БД як `timestamp without time zone`;
- при створенні бронювання час нормалізується до `DateTimeKind.Unspecified`;
- доступний зал повинен бути активним, мати достатню місткість і не перетинатися з іншими бронюваннями, що не мають статус `Cancelled`.

## Початкові Дані

У `AppDbContext` задано seed-дані для старту:

- зали: `Зал А`, `Зал B`, `Зал C`;
- опції: `Проектор`, `Wi-Fi`, `Звук`.
