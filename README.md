[🇺🇸 English](README.md) | [🇻🇳 Tiếng Việt](README.vi.md)

# Theater Management System - Backend

The backend for the Theater Management System is built with ASP.NET Core Web API and PostgreSQL.

## 🛠 Prerequisites

- **[.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
- **[PostgreSQL](https://www.postgresql.org/download/)**
- **[pgAdmin 4](https://www.pgadmin.org/download/)** (Optional, for database management)

## 🚀 Setup & Installation

### 1. Database Setup

1.  Install PostgreSQL.
2.  Create a new database named `theater_management`.
3.  Update the connection string in `appsettings.Development.json` (or `appsettings.json`) to point to your local PostgreSQL instance:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=theater_management;Username=postgres;Password=your_password"
    }
    ```

### 2. Run the Application

1.  Open a terminal in this directory.
2.  Restore dependencies:
    ```bash
    dotnet restore
    ```
3.  Apply database migrations:
    ```bash
    dotnet run -- migrate
    ```
4.  Start the server:
    ```bash
    dotnet run
    ```
    The API will be available at `http://localhost:5000` (or the port configured in `launchSettings.json`).
