# Multi-Tenant Web API Documentation

## Overview
This is a secure and maintainable Web API that supports multi-tenancy within a single shared PostgreSQL database. Each tenant's data is strictly isolated at the application level. The solution uses ASP.NET Core 8, follows Clean Architecture principles, and utilizes modern development practices.

## Architecture
The solution follows Clean Architecture with the following layers:

### 1. Domain Layer (`SumXAssignment.Domain`)
- **Entities**: Domain entities (ETenant, EUser, EEmployee)
- **Interfaces**: Repository interfaces for commands and queries

### 2. Application Layer (`SumXAssignment.Application`)
- **DTOs**: Data Transfer Objects for requests and responses
- **Managers**: Business logic handlers
- **CQRS**: Command Query Responsibility Segregation pattern

### 3. Infrastructure Layer (`SumXAssignment.Infrastructure`)
- **Data Access**: Entity Framework Core implementations
- **Services**: Command and Query service implementations
- **Database Context**: PostgreSQL database context

### 4. API Layer (`SumXAssessment`)
- **Controllers**: RESTful API endpoints
- **Authentication**: JWT-based authentication
- **Authorization**: Role-based access control

## Database Schema

### Tenants Table
- `Id`: string (Primary Key)
- `Name`: string (not null)
- `EmailAddress`: string (not null)
- `TenantId`: string(2) (unique, auto-incremented like T1, T2, etc.)

### Users Table (ASP.NET Core Identity)
- Inherits from `IdentityUser`
- `TenantId`: string (nullable, Foreign Key to Tenants.Id)

### Employees Table
- `Id`: string (Primary Key)
- `FullName`: string (not null)
- `EmailAddress`: string (not null)
- `TenantId`: string (not null, Foreign Key to Tenants.Id)

## Authentication & Authorization

### Default Admin User
- **Email**: `admin@system.com`
- **Password**: `Admin@123`
- **Role**: `Admin`

### Tenant Users
- **Username**: Tenant's email address
- **Password**: `Tenant` + `TenantId` (e.g., `TenantT1` for tenant with ID T1)
- **Role**: `Tenant`

### JWT Configuration
The API uses JWT tokens for authentication. Configure the following in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your-secret-key-here",
    "Issuer": "SumXAssessment",
    "Audience": "SumXAssessment"
  }
}
```

## API Endpoints

### Authentication
- `POST /api/v1/auth/login` - User login (returns JWT token)

### Tenant Management (Admin Only)
- `POST /api/v1/tenant/CreateTenant` - Create new tenant

### User Registration (Tenant Users Only)
- `POST /api/v1/user/register` - Register new user within tenant

### Employee Management (Tenant Users Only)
- `POST /api/v1/employee` - Create employee
- `GET /api/v1/employee` - Get all employees (tenant-scoped)
- `GET /api/v1/employee/{id}` - Get employee by ID (tenant-scoped)
- `PUT /api/v1/employee/{id}` - Update employee (tenant-scoped)
- `DELETE /api/v1/employee/{id}` - Delete employee (tenant-scoped)

## Multi-Tenancy Implementation

### Data Isolation
- All employee operations are automatically scoped to the authenticated user's tenant
- Tenant ID is extracted from JWT claims
- Database queries include tenant filtering at the application level

### Security Features
- Role-based authorization (Admin, Tenant)
- JWT token authentication
- Tenant data isolation
- Secure password hashing using ASP.NET Core Identity

## Usage Flow

### 1. Admin Operations
1. Login as admin using default credentials
2. Create tenants using the tenant creation endpoint
3. Each tenant creation automatically creates a default tenant user

### 2. Tenant Operations
1. Login as tenant user (email from tenant, password = "Tenant" + TenantId)
2. Register additional users within the tenant
3. Manage employees (CRUD operations)
4. All operations are automatically scoped to the tenant

### 3. Employee Management
- Create, Read, Update, Delete employees
- All operations respect tenant boundaries
- Employees can only be accessed by users from the same tenant

## Getting Started

### Prerequisites
- .NET 8.0 or higher
- PostgreSQL database
- Visual Studio 2022 or VS Code

### Configuration
1. Update connection string in `appsettings.json`
2. Configure JWT settings
3. Run Entity Framework migrations
4. Start the application

### Database Setup
```bash
dotnet ef database update
```

### Running the Application
```bash
dotnet run --project SumXAssessment
```

## Security Considerations

### Authentication
- JWT tokens with configurable expiration
- Secure password hashing using Identity
- Role-based access control

### Authorization
- Admin-only tenant management
- Tenant-scoped user registration
- Tenant-scoped employee operations

### Data Protection
- Tenant data isolation at application level
- Parameterized queries to prevent SQL injection
- Input validation using Data Annotations

## Testing

Use the provided `API-Testing.http` file for testing endpoints with sample requests.

### Testing Flow
1. Login as admin
2. Create a tenant
3. Login as tenant user
4. Test user registration and employee CRUD operations

## Dependencies

### Core Dependencies
- ASP.NET Core 8.0
- Entity Framework Core
- ASP.NET Core Identity
- Npgsql (PostgreSQL provider)

### Authentication
- JWT Bearer Authentication
- Microsoft.IdentityModel.Tokens

### Database
- PostgreSQL
- Entity Framework Core migrations

## Error Handling
- Consistent error responses using ResponseStatus<T>
- Proper HTTP status codes
- Detailed error messages for development

## Performance Considerations
- Database indexing on tenant foreign keys
- Efficient queries with proper filtering
- Async/await pattern throughout

## Future Enhancements
- Implement additional tenant management features
- Add audit logging
- Implement rate limiting
- Add API versioning
- Implement caching strategies