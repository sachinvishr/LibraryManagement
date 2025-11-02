# Library Management System API

A comprehensive Library Management System built with ASP.NET Core Web API, Entity Framework Core, and SQLite. This system provides complete functionality for managing books, members, borrowing operations, and generating insightful reports.

## 📋 Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Database Configuration](#database-configuration)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

- **Book Management**: Add, retrieve, and track book inventory
- **Member Management**: Register and manage library members
- **Borrowing System**: Handle book checkouts with availability tracking
- **Return Processing**: Process book returns and update availability
- **Overdue Reporting**: Automatically identify overdue books (14-day loan period)
- **Analytics**: Generate reports on most borrowed books
- **Data Validation**: Built-in validation for all operations
- **Interactive Documentation**: Swagger/OpenAPI integration
- **Database Migrations**: Version-controlled database schema

## 🛠 Technology Stack

- **Framework**: ASP.NET Core Web API
- **ORM**: Entity Framework Core
- **Database**: SQLite
- **Documentation**: Swagger/OpenAPI (Swashbuckle)
- **Language**: C#

## 📁 Project Structure

```
LibraryManagement/
├── src/
│   └── LibraryManagement.Api/
│       ├── Controllers/        # API endpoint controllers
│       ├── DTOs/              # Data Transfer Objects
│       ├── Entities/          # Database models
│       ├── Data/              # DbContext and configurations
│       ├── Migrations/        # EF Core migrations
│       ├── appsettings.json   # Application configuration
│       ├── Program.cs         # Application entry point
│       └── Library.db         # SQLite database file
├── LibraryManagement.sln      # Solution file
└── README.md                  # This file
```

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or later
- [Git](https://git-scm.com/downloads) (for cloning the repository)
- A code editor (recommended: [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/))

## 🚀 Installation & Setup

### Step 1: Create Solution and Project

```bash
# Create project directory
mkdir LibraryManagement
cd LibraryManagement

# Create solution file
dotnet new sln -n LibraryManagement

# Create Web API project
dotnet new webapi -n LibraryManagement.Api -o src/LibraryManagement.Api

# Add project to solution
dotnet sln add src/LibraryManagement.Api/LibraryManagement.Api.csproj
```

### Step 2: Install Dependencies

Navigate to the API project directory:

```bash
cd src/LibraryManagement.Api
```

Install required NuGet packages:

```bash
# Entity Framework Core with SQLite support
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Swagger/OpenAPI documentation
dotnet add package Swashbuckle.AspNetCore
```

Install Entity Framework Core CLI tools globally:

```bash
dotnet tool install --global dotnet-ef
```

### Step 3: Implement Core Components

Create the following components in your project:

#### **Entities** (Database Models)
- `Book.cs` - Book entity with properties (Id, Title, Author, ISBN, IsAvailable)
- `Member.cs` - Member entity (Id, Name, Email, MembershipDate)
- `BorrowRecord.cs` - Borrowing transaction record (Id, BookId, MemberId, BorrowDate, ReturnDate)

#### **Data Context**
- `Data/LibraryDbContext.cs` - EF Core DbContext with DbSets for all entities

#### **DTOs** (Data Transfer Objects)
Create request/response models in the `DTOs/` folder for API communication

#### **Controllers**
- `BooksController` - Book management endpoints
- `MembersController` - Member management endpoints
- `BorrowController` - Book borrowing operations
- `ReturnController` - Book return operations
- `ReportsController` - Analytics and reporting endpoints

## ⚙️ Database Configuration

### Step 1: Configure Connection String

Update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Library.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Step 2: Configure Services in Program.cs

Ensure your `Program.cs` includes:

```csharp
// Entity Framework Core
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers
builder.Services.AddControllers();
app.MapControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

### Step 3: Create and Apply Migrations

Return to the project root directory and run:

```bash
# Create initial migration
dotnet ef migrations add InitialCreate \
  --project src/LibraryManagement.Api \
  --startup-project src/LibraryManagement.Api

# Apply migration to create database
dotnet ef database update \
  --project src/LibraryManagement.Api \
  --startup-project src/LibraryManagement.Api
```

### Step 4: Build the Project

```bash
# Clean previous builds
dotnet clean

# Build the solution
dotnet build
```

## ▶️ Running the Application

### Local Development

```bash
cd src/LibraryManagement.Api
dotnet run
```

The API will start and listen on HTTPS (typically port 5196).

### Accessing the Application

- **Swagger UI (Local)**: `https://localhost:5196/swagger/index.html`
- **GitHub Codespaces**: `https://fantastic-guacamole-7vj69p797r6qcrxx-5196.app.github.dev/swagger/index.html`

> **Note**: Replace the port number if your application uses a different port. Check the console output after running `dotnet run`.

## 📖 API Documentation

Interactive API documentation is available through Swagger UI. Once the application is running, navigate to the Swagger endpoint to:

- View all available endpoints
- Test API calls directly from the browser
- See request/response schemas
- Review data models

## 🔌 API Endpoints

### 📚 Books Management

| Method | Endpoint          | Description                 |
|--------|-------------------|-----------------------------|
| GET    | `/api/books`      | Retrieve all books          |
| GET    | `/api/books/{id}` | Retrieve a specific book    |
| POST   | `/api/books`      | Add a new book              |

### 👥 Members Management

| Method | Endpoint            | Description                 |
|--------|---------------------|-----------------------------|
| GET    | `/api/members/{id}` | Retrieve a specific member  |
| POST   | `/api/members`      | Register a new member       |

### 📤 Borrowing Operations

| Method | Endpoint         | Description                    |
|--------|------------------|--------------------------------|
| POST   | `/api/borrow`    | Borrow a book                  |
| GET    | `/api/borrow/member/{memberId}` | Get specific borrowing record |

### 📥 Return Operations

| Method | Endpoint       | Description              |
|--------|----------------|--------------------------|
| POST   | `/api/return`  | Return a borrowed book   |

### 📊 Reports & Analytics

| Method | Endpoint                    | Description                               |
|--------|-----------------------------|-------------------------------------------|
| GET    | `/api/reports/overdue`      | Get list of overdue books (>14 days)     |
| GET    | `/api/reports/top-borrowed` | Get most frequently borrowed books        |

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 🆘 Troubleshooting

### Common Issues

**Issue**: `dotnet ef` command not found  
**Solution**: Install EF Core tools globally: `dotnet tool install --global dotnet-ef`

**Issue**: Database connection errors  
**Solution**: Ensure the connection string in `appsettings.json` is correct and the database file has proper permissions

**Issue**: Port already in use  
**Solution**: Change the port in `launchSettings.json` or stop the application using the port
