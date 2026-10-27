<h1>TMS.API - Task Management System</h1>

📌 Overview

TMS.API is a robust and scalable Task Management System API built with .NET 8 and Entity Framework Core. It leverages JWT-based authentication with refresh token support and follows clean architecture principles for maintainability and scalability.

<h2>🏗️ Architecture & Principles</h2>
<ol>
<li>Clean Architecture

Presentation Layer: Handles API requests using ASP.NET Core Controllers.

Application Layer: Contains business logic and service implementations.

Domain Layer: Core domain models and business rules.

Infrastructure Layer: Handles database access (EF Core with PostgreSQL), authentication, and external services.
</li>

<li>Key Principles

Separation of Concerns: Each layer has a distinct responsibility.

Dependency Injection: Services and repositories are injected for better testability.

Security: Implements JWT authentication and refresh tokens for secure access.

Scalability: Supports horizontal scaling with a structured database.
</li>
</ol>

<h2>🚀 Features</h2>
<ul>
 <li> ✅ User Authentication (JWT & Refresh Token Support)</li>
 <li> ✅ Role-Based Authorization</li>
 <li> ✅ Task Management (CRUD operations for tasks)</li>
 <li> ✅ Secure API Endpoints</li>
 <li> ✅ PostgreSQL Database Integration</li>
 <li> ✅ Entity Framework Core for ORM</li>
</ul>

<h2>🧭 Getting Started</h2>

Requirements: .NET 8 SDK and a PostgreSQL instance.

<pre>
dotnet restore
dotnet ef database update --project TMS.INFRASTRUCTURE --startup-project TMS.API
dotnet run --project TMS.API
</pre>

Set the connection string and JWT settings in <code>TMS.API/appsettings.Development.json</code> or user secrets.

<h2>🔌 Endpoints</h2>
<ul>
 <li><code>POST /api/users/signup</code>, <code>POST /api/users/signin</code>, <code>GET /api/users/{userId}</code></li>
 <li><code>GET /api/tasks/{taskId}</code>, <code>POST</code>, <code>PUT</code>, <code>DELETE /api/tasks</code></li>
</ul>
