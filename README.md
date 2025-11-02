# LibraryManagement

📚 LibraryManagement API

A complete Library Management System built using ASP.NET Core Web API, Entity Framework Core, and SQLite, featuring Books, Members, Borrowing, Returning, Reports, Validation, and Swagger Documentation.

🚀 Features

✅ Add / Get Books
✅ Add / Get Members
✅ Borrow Books
✅ Return Books
✅ Track Availability
✅ Overdue Report (BorrowDate + 14 days)
✅ Top Borrowed Books Report
✅ Full CRUD Flow
✅ Swagger UI
✅ EF Core Migrations
✅ SQLite Database

🧱 Project Architecture
Web API  →  Entity Framework Core  →  SQLite Database

📁 Folder Structure
LibraryManagement/
 ├─ src/
 │   └─ LibraryManagement.Api/
 │       ├── Controllers/
 │       ├── DTOs/
 │       ├── Entities/
 │       ├── Data/
 │       ├── Migrations/
 │       ├── appsettings.json
 │       ├── Program.cs
 │       └── Library.db
 ├─ LibraryManagement.sln
 └─ README.md

✅ Setup & Run Guide (Step-by-Step)
✅ STEP 1 — Create Solution & API Project

mkdir LibraryManagement
cd LibraryManagement

dotnet new sln -n LibraryManagement

dotnet new webapi -n LibraryManagement.Api -o src/LibraryManagement.Api

dotnet sln add src/LibraryManagement.Api/LibraryManagement.Api.csproj

✅ STEP 2 — Install Required Packages

Navigate to API folder:

cd src/LibraryManagement.Api


Install EF + SQLite + Swagger:

dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Swashbuckle.AspNetCore


Install EF CLI:

dotnet tool install --global dotnet-ef

✅ STEP 3 — Add Entities

Create files:

Book.cs

Member.cs

BorrowRecord.cs

✅ STEP 4 — Add DbContext

Add: Data/LibraryDbContext.cs

✅ STEP 5 — Configure SQLite in appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=Library.db"
}

✅ STEP 6 — Configure Program.cs

Add:

✅ EF Core
✅ Swagger
✅ Controllers
✅ ConnectionString

✅ STEP 7 — Add DTOs

Stored in /DTOs

✅ STEP 8 — Add Controllers

BooksController

MembersController

BorrowController

ReturnController

ReportsController

✅ STEP 9 — Clean + Build
dotnet clean
dotnet build

✅ STEP 10 — Create Database

From project root:

dotnet ef migrations add InitialCreate \
  --project src/LibraryManagement.Api \
  --startup-project src/LibraryManagement.Api

dotnet ef database update \
  --project src/LibraryManagement.Api \
  --startup-project src/LibraryManagement.Api

✅ STEP 11 — Run the API
cd src/LibraryManagement.Api
dotnet run

✅ STEP 12 — Open Swagger UI

✅ Local Machine:

https://localhost:5196/swagger/index.html


✅ GitHub Codespaces:

https://fantastic-guacamole-7vj69p797r6qcrxx-5196.app.github.dev/swagger/index.html

📌 API Endpoints

📘 Books API

| Method | Endpoint          | Description    |
| ------ | ----------------- | -------------- |
| POST   | `/api/books`      | Add Book       |
| GET    | `/api/books`      | Get All Books  |
| GET    | `/api/books/{id}` | Get Book by ID |

👤 Members API

| Method | Endpoint            | Description |
| ------ | ------------------- | ----------- |
| POST   | `/api/members`      | Add Member  |
| GET    | `/api/members/{id}` | Get Member  |

