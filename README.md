# Authentication and Authorization Service API

## Overview
This is a scalable backend API built using **.NET Core** and **MSSQL** that manages user authentication, authorization, and email notifications. It implements **Role-Based Access Control (RBAC)** for Admin and User roles and includes logging and error handling mechanisms.

## Features

### Authentication
- User signup
- User login
- Get authenticated user details

### Authorization
- Role-Based Access Control (RBAC) for Admin and User roles
- Role management

### Email Notification
- Modular email notification system with multiple implementations
- Supports **SMTP (Google, Office365)** and **SendGrid**
- Configurable active provider

### Third-Party Integration
- Two API endpoints protected with API keys/secrets

### Logging
- Implemented using **Serilog/NLog**
- Logs important events and errors

### Database
- **MSSQL** as the database management system
- Tables: Users, Roles

## Setup Instructions

### Prerequisites
- .NET Core (.NET 8)
- MSSQL Server
- Postman (for API testing) or any API testing tool
- Git (for cloning the repository)

### Installation
1. **Clone the repository:**
   ```sh
   git clone https://github.com/Stanley-Ugo/AuthenticationAndAuthorizationServiceAPI.git
   cd AuthenticationAndAuthorizationServiceAPI
   ```

2. **Set up the database:**
   - Update the **appsettings.json** file with your MSSQL connection string.
   - Run database migrations:
     ```sh
     dotnet ef database update
     ```

3. **Configure Email Provider:**
   - Set up either **SMTP (Google/Office365)** or **SendGrid** in the configuration.

4. **Run the API:**
   ```sh
   dotnet run
   ```

5. **API Endpoints:**
   - The API documentation is available via Swagger at:
     ```
     http://localhost:{PORT}/swagger
     ```

## Testing
- Feature tests are implemented using **xUnit**.
- Run tests with:
  ```sh
  dotnet test
  ```

## Security Considerations
- Sensitive configuration values (e.g., API keys, database credentials) should be stored in environment variables.
- API key protection for third-party endpoints.


For any further inquiries, feel free to reach out.

---
**Author:** Stanley Umeh

