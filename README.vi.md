[🇺🇸 English](README.md) | [🇻🇳 Tiếng Việt](README.vi.md)

# Hệ thống Quản lý Rạp chiếu phim - Backend

Backend cho Hệ thống Quản lý Rạp chiếu phim được xây dựng bằng ASP.NET Core Web API và PostgreSQL.

## 🛠 Yêu cầu tiên quyết

- **[.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
- **[PostgreSQL](https://www.postgresql.org/download/)**
- **[pgAdmin 4](https://www.pgadmin.org/download/)** (Tùy chọn, để quản lý cơ sở dữ liệu)

## 🚀 Cài đặt & Thiết lập

### 1. Thiết lập Cơ sở dữ liệu

1.  Cài đặt PostgreSQL.
2.  Tạo một cơ sở dữ liệu mới có tên `theater_management`.
3.  Cập nhật chuỗi kết nối trong `appsettings.Development.json` (hoặc `appsettings.json`) để trỏ đến instance PostgreSQL cục bộ của bạn:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=theater_management;Username=postgres;Password=your_password"
    }
    ```

### 2. Chạy Ứng dụng

1.  Mở terminal tại thư mục này.
2.  Khôi phục các gói phụ thuộc:
    ```bash
    dotnet restore
    ```
3.  Áp dụng migration cơ sở dữ liệu:
    ```bash
    dotnet run -- migrate
    ```
4.  Khởi động server:
    ```bash
    dotnet run
    ```
    API sẽ có sẵn tại `http://localhost:5000` (hoặc cổng được cấu hình trong `launchSettings.json`).
