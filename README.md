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
