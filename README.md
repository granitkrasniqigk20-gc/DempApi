# DempApi

A .NET 8 RESTful API built with Clean Architecture for managing employees and master data (departments and positions).

## Architecture

This project follows Clean Architecture principles with the following layers:

- **Domain Layer** (`DempApi.Domain`): Contains enterprise business rules and entities
- **Application Layer** (`DempApi.Application`): Contains application business rules, DTOs, interfaces, and services
- **Infrastructure Layer** (`DempApi.Infrastructure`): Contains data access implementation with Entity Framework Core
- **API Layer** (`DempApi.API`): Contains Web API controllers and configuration

## Features

- ✅ Employee Management (CRUD operations)
- ✅ Department Management (Master Data)
- ✅ Position Management (Master Data)
- ✅ RESTful API with Swagger documentation
- ✅ Entity Framework Core with SQLite
- ✅ Clean Architecture
- ✅ Dependency Injection
- ✅ Error handling and logging

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQLite
- Swagger/OpenAPI

## Getting Started

### Prerequisites

- .NET 8 SDK or later

### Running the Application

1. Clone the repository:
```bash
git clone https://github.com/granitkrasniqigk20-gc/DempApi.git
cd DempApi
```

2. Build the solution:
```bash
dotnet build
```

3. Run the API:
```bash
cd DempApi.API
dotnet run
```

The API will start on `http://localhost:5130` (or the port configured in `launchSettings.json`).

4. Access Swagger UI:
   - Navigate to `http://localhost:5130/swagger` in your browser

## API Endpoints

### Employees

- `GET /api/employees` - Get all employees
- `GET /api/employees/{id}` - Get employee by ID
- `POST /api/employees` - Create a new employee
- `PUT /api/employees/{id}` - Update an employee
- `DELETE /api/employees/{id}` - Delete an employee

### Departments

- `GET /api/departments` - Get all departments
- `GET /api/departments/{id}` - Get department by ID
- `POST /api/departments` - Create a new department
- `PUT /api/departments/{id}` - Update a department
- `DELETE /api/departments/{id}` - Delete a department

### Positions

- `GET /api/positions` - Get all positions
- `GET /api/positions/{id}` - Get position by ID
- `POST /api/positions` - Create a new position
- `PUT /api/positions/{id}` - Update a position
- `DELETE /api/positions/{id}` - Delete a position

## Example API Calls

### Create an Employee

```bash
curl -X POST http://localhost:5130/api/employees \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "phoneNumber": "+1234567890",
    "dateOfBirth": "1990-05-15",
    "hireDate": "2024-01-01",
    "departmentId": 1,
    "positionId": 2
  }'
```

### Get All Employees

```bash
curl http://localhost:5130/api/employees
```

## Database

The application uses SQLite as the database provider. The database file (`dempapi.db`) will be created automatically when you run the application for the first time.

### Seeded Data

The database is seeded with initial master data:

**Departments:**
- IT - Information Technology
- HR - Human Resources
- Finance - Finance Department

**Positions:**
- Software Engineer
- Senior Software Engineer
- HR Manager
- Accountant

## Project Structure

```
DempApi/
├── DempApi.Domain/           # Domain entities and business rules
│   └── Entities/
│       ├── BaseEntity.cs
│       ├── Employee.cs
│       ├── Department.cs
│       └── Position.cs
├── DempApi.Application/      # Application logic and DTOs
│   ├── DTOs/
│   ├── Interfaces/
│   └── Services/
├── DempApi.Infrastructure/   # Data access and EF Core
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   └── Repositories/
│       └── Repository.cs
└── DempApi.API/              # Web API controllers
    └── Controllers/
        ├── EmployeesController.cs
        ├── DepartmentsController.cs
        └── PositionsController.cs
```

## Clean Architecture Benefits

1. **Independence of Frameworks**: Business logic doesn't depend on the existence of some library of feature-laden software
2. **Testability**: Business rules can be tested without the UI, Database, Web Server, or any other external element
3. **Independence of UI**: The UI can change easily, without changing the rest of the system
4. **Independence of Database**: You can swap out SQLite for SQL Server, Oracle, MongoDB, or any other database
5. **Independence of any external agency**: Business rules don't know anything at all about the outside world

## License

This project is licensed under the MIT License.