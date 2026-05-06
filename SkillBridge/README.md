# SkillBridge

SkillBridge is a full-stack starter for an online courses platform built with:

- Frontend: React + Vite
- Backend: ASP.NET Core Web API
- Database: SQL Server with EF Core
- Authentication: JWT + ASP.NET Core Identity
- API documentation: Swagger

## Project structure

```text
SkillBridge/
├── backend/
│   └── SkillBridge.Api/
└── frontend/
    └── skillbridge-client/
```

## Backend setup

1. Open a terminal in `SkillBridge/backend/SkillBridge.Api`
2. Update the SQL Server connection string in `appsettings.json`
3. Restore packages, create the database, and run the API

### Backend commands

```powershell
cd "E:\Online-Platform Project\Online-Platform\SkillBridge\backend\SkillBridge.Api"
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

The API is configured to run on `http://localhost:5000`.

Swagger will be available at:

- `http://localhost:5000/swagger`

## Frontend setup

1. Open a terminal in `SkillBridge/frontend/skillbridge-client`
2. Install dependencies
3. Start the Vite development server

### Frontend commands

```powershell
cd "E:\Online-Platform Project\Online-Platform\SkillBridge\frontend\skillbridge-client"
npm install
npm run dev
```

The frontend runs at:

- `http://localhost:5173`

## Database connection string

The default backend connection string is stored in:

- `SkillBridge/backend/SkillBridge.Api/appsettings.json`

Default value:

```json
"DefaultConnection": "Server=localhost;Database=SkillBridgeDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

If you use SQL Server authentication instead of Windows authentication, replace it with something like:

```json
"DefaultConnection": "Server=localhost;Database=SkillBridgeDb;User Id=sa;Password=YourStrongPassword;TrustServerCertificate=True"
```

## EF Core migrations

Use these commands from the backend project folder:

```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Default admin account

The backend seeds one admin user and the required roles on startup.

- Email: `admin@skillbridge.com`
- Password: `Admin@12345`

## Included starter features

- Identity user model with roles: Admin, Instructor, Student
- JWT authentication setup
- SQL Server EF Core context and entity relationships
- Role and admin seed helper
- Swagger with bearer token support
- React routing for public, student, instructor, and admin areas
- Shared axios client and auth context
- Placeholder pages and layouts that compile and run

## Recommended next step

Implement the course creation and listing flow end to end:

1. Create the first EF Core migration and database.
2. Add validation to the backend DTOs.
3. Connect the frontend course pages to the live API.
4. Build the instructor create and edit course forms.
