# Inventory Management System

A .NET solution demonstrating CRUD (Create, Read, Update, Delete) operations for product management using clean architecture principles. The system allows basic product inventory tracking through a Blazor front-end interfacing with an ASP.NET Core Web API.

## Project Architecture

The solution follows a clean architecture approach with clear separation of concerns:

```
Solution Structure
├── InventoryManagement.Data        # Database and entity management
├── InventoryManagement.Server      # Core business logic
├── InventoryManagement.Api         # RESTful API endpoints
├── InventoryManagement.Web         # Blazor UI
├── InventoryManagement.Shared      # Cross-cutting concerns
└── InventoryManagement.Tests       # Automated testing
```

### Implementation Highlights

- **Clean Architecture**: Each layer has a single responsibility, making the code maintainable and testable
- **Modern Tech Stack**: Built with .NET 8, Entity Framework Core, and Blazor
- **API Documentation**: Swagger integration for easy API testing
- **Automated Tests**: In-memory database testing for business logic validation

## Running the Project

1. **Prerequisites**
   - .NET 8.0 SDK
   - SQL Server

2. **Setup**
   ```bash
   git clone https://github.com/MarilynHb/InventoryManagement.git
   dotnet restore
   dotnet ef database update --project InventoryManagement.Data
   ```

3. **Launch**
   - Start both API and Blazor projects
   - Access Swagger UI: https://localhost:[port]/swagger
   - Access Blazor UI: https://localhost:[port]

**Note**: For development, you may need to update the CORS ports in Program.cs of the API project to match your Blazor project's port.

## Current Limitations and Development Notes

During development, I identified several areas that could be improved:

1. **Performance Considerations**
   - Product filtering currently queries the server repeatedly even when data is locally available
   - Initial load fetches the entire dataset; pagination would be more efficient
   - No loading indicators during data fetches

2. **Data Validation**
   - Server-side uniqueness check needed for product codes during updates

3. **Security & Access Control**
   - Authentication and authorization needed for data access control
   - Role-based permissions for different operations

4. **User Experience**
   - Consider adding explicit data load controls instead of automatic loading on navigation
   - Add loading indicators for network operations

----
This project represents my approach to implementing a maintainable and well-structured .NET solution. While the functionality is focused on basic CRUD operations, the architecture is designed to support future enhancements and maintain code quality.
