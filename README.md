# TechMove Logistics: Global Logistics Management System (GLMS)

## 📌 Project Overview
This is a **Global Logistics Management System (GLMS)** built for TechMove Logistics to streamline their international contract and service request management. The system provides a centralized hub for managing clients, tracking contract SLAs, and handling financial conversions.

## 🚀 Key Features
- **Contract Management Hub:** CRUD operations for logistics agreements.
- **Service Level Agreements (SLA):** Robust validation ensures service requests are only raised against active contracts.
- **Financial Integration:** Real-time USD to ZAR currency conversion via external API.
- **Secure File Handling:** PDF agreement uploads with unique UUID renaming to prevent data loss.
- **Unit Testing:** Comprehensive test suite using xUnit and Moq for business logic validation.

## 🛠️ Tech Stack
- **Framework:** .NET 8.0 (ASP.NET Core MVC)
- **Database:** SQL Server via Entity Framework Core
- **Design Patterns:** Repository Pattern, Service-Oriented Architecture, Dependency Injection
- **Testing:** xUnit, Moq
- **Frontend:** Bootstrap 5, Razor Views

## ⚙️ Setup Instructions

### 1. Prerequisites
- Visual Studio 2022 (v17.8+)
- SQL Server Express / LocalDB

### 2. Database Configuration
Update the `DefaultConnection` string in `appsettings.json` to point to your local SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TechMove_GLMS;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 3. API Key Setup
This project uses **ExchangeRate-API** for currency conversion. Add your API key to `appsettings.json`:
```json
"CurrencySettings": {
  "ApiKey": "YOUR_KEY_HERE"
}
```

### 4. Running the Project
1. Open the Package Manager Console.
2. Run `Update-Database` to create the schema.
3. Press `F5` to launch the application.

## 🧪 Running Tests
1. Open the **Test Explorer** in Visual Studio.
2. Click **Run All Tests**.
3. All tests should pass using mocked data (no database required for tests).

## 📂 Project Structure
- `GLMS.Web`: Main MVC application, including Services and Repositories.
- `GLMS.Tests`: Unit testing project targeting business logic.
- `wwwroot/uploads/contracts`: Directory for stored PDF agreements.

---
*Developed as part of the Portfolio of Evidence (PoE) for TechMove Logistics.*
