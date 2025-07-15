SumX Assessment for .NET Developer includes: 


-Once the project is cloned and open, add migration :

-------------add-migration-----------

and then update the db : 
-----------update-database----

--Use SumX branch in git.
-Rebuild the solution and run the project. 
-Core: domain Entities, business rules.
-Application: Implemented CQRS, DTOs, service Interfaces.
-Infrastructure: Set up data access using Entity Framework Core and configure
    Microsoft Identity for authentication and authorization.
-API:RESTful endpoints through Controller.
-Used EF core code first migrations for PostgreSQL schema.
-Required tables: Tenants, Users and Employees.
-Implemented endpoints and CRUD for Tenants
-Only Admin users may access these endpoints. The credentials are :: 

------Admin Login: admin@system.com / Admin@123
------Create Tenant: Use admin token
------Tenant Login: Use generated password (e.g., TenantT1)

-Applied the CQRS pattern: use separate handlers for commands (write operations)
and queries (read operations).
-Used ASP.NET Core's built-in Dependency Injection for all services.
-Clean code organization, followed Clean Architecture and CQRS principles, and proper use of Dependency Injection.

