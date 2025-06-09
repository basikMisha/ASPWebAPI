# Pet Center API

This is a .NET Web API project for managing a pet adoption center. It provides endpoints to manage pets, adopters, volunteers, adoption requests, and user authentication using JWT.

---

Tech Stack
ASP.NET Core 8

Dapper & EF Core (switchable)

FluentValidation

AutoMapper

Microsoft SQL Server

BCrypt

Swagger / OpenAPI

JWT Authentication

##  Features

- RESTful API for pet adoption workflows
- Role-based access control (`Admin`, `User`)
- JWT authentication with refresh tokens
- Switchable data access layer: **Dapper** or **Entity Framework Core**
- Clean N-Layer architecture (`API`, `BLL`, `DAL`, `Domain`, `DTOs`)
- Validation using FluentValidation
- Swagger UI for API testing
- AutoMapper support

---

##  Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/ASPWebAPI.git
cd ASPWebAPI
```
2. Create the Database
Run the SQL script to set up the initial database schema:

Open SQL Server Management Studio (or any SQL tool)

Execute the script located at: Infrastructure/Scripts/db.sql
This script will:

Create the database PETCENTER

Create schemas: roles, auth, adoption

Create all necessary tables and indexes
3. Configure JWT Authentication Key
This project uses JWT for secure user authentication. You must configure a secret key using User Secrets:
```bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "MySuperSecretKey123"
```
Alternatively, in Visual Studio, right-click on the ASPWebAPI.Api project and select "Manage User Secrets".

Note: The secret should be at least 64 characters long for HMACSHA512 security.

4. Choose Data Access Strategy
You can switch between Dapper and EF Core by modifying appsettings.json:
```bash
"UseEfDal": true
```
true → Use Entity Framework Core (default option)

false → Use Dapper
5. Run the Application
```bash
dotnet run --project ASPWebAPI.Api
```
Once running, navigate to:
```bash
https://localhost:5001/swagger
```
6. Default Admin Account
On first launch, the application seeds a default admin account if no admin exists.

Email: admin@example.com

Password: Admin123

This enables immediate access to protected endpoints.

Seeding is implemented in Infrastructure/DatabaseSeeder.cs using Dapper.
