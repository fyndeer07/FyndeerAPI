# CLAUDE.md — Project Architecture & Conventions

This file defines the architecture, tooling decisions, and conventions for this project.
Claude and all contributors must follow these guidelines consistently.

---

## Stack Overview

| Concern | Technology |
|---|---|
| Runtime | .NET 10 |
| Language | C# (latest version) |
| API Style | REST — Controllers |
| ORM | Entity Framework Core |
| Migrations | FluentMigrator |
| Messaging | MediatR |
| Logging | Serilog |
| API Docs | Scalar (all environments) |
| Local Database | Azure SQL Edge (Docker) |
| Production Database | Azure SQL Serverless |
| Hosting | Azure Container Apps |
| CI/CD | GitHub Actions |
| Image Registry | GitHub Container Registry (ghcr.io) |
| Secrets | Azure Key Vault (production), appsettings.Development.json (local) |

---

## Project Structure

The solution follows **Clean Architecture** with four projects:

```
FyndeerAPI/
├── FyndeerAPI.Api/                  # Entry point — controllers, middleware, DI wiring
├── FyndeerAPI.Application/          # Use cases — MediatR handlers, commands, queries, DTOs
├── FyndeerAPI.Domain/               # Core business — entities, value objects, domain events
├── FyndeerAPI.Infrastructure/       # External concerns — EF Core, FluentMigrator, repositories
└── FyndeerAPI.slnx
```

### Layer Responsibilities

**FyndeerAPI.Domain**
- Entities, value objects, enums, domain events
- No dependencies on other projects or external packages
- Pure C# only

**FyndeerAPI.Application**
- MediatR commands, queries, and handlers. Dont separate Command,queries and handlers in different files
- DTOs (request/response models)
- Interfaces (e.g. `IAppDbContext`) — implemented in Infrastructure
- Depends on: `Domain` only

**FyndeerAPI.Infrastructure**
- EF Core `AppDbContext` and entity configurations
- FluentMigrator migrations
- External service integrations
- Depends on: `Application`, `Domain`

**FyndeerAPI.Api**
- Controllers — thin, delegate to MediatR
- Middleware (exception handling, logging)
- Dependency injection wiring (`Program.cs`)
- Depends on: `Application`, `Infrastructure`

---

## Naming Conventions

### General Rules
- Use **PascalCase** for classes, methods, properties, and constants
- Use **camelCase** for local variables and parameters
- Use **_camelCase** (underscore prefix) for private fields
- Avoid abbreviations — prefer clarity over brevity
- Names must describe intent, not implementation
- Don't use Primary constructors. Always prefer block body for constructors

### Files and Classes

| Type | Convention | Example |
|---|---|---|
| Entity | `{Name}` | `User`, `Order` |
| Controller | `{Name}Controller` | `UsersController` |
| MediatR Command | `{Action}{Entity}Command` | `CreateUserCommand` |
| MediatR Query | `{Action}{Entity}Query` | `GetUserByIdQuery` |
| MediatR Handler | `{Command/Query}Handler` | `CreateUserCommandHandler` |
| Response DTO | `{Name}Response` | `UserResponse` |
| Request DTO | `{Name}Request` | `CreateUserRequest` |
| Repository Interface | `I{Name}Repository` | `IUserRepository` |
| Repository Implementation | `{Name}Repository` | `UserRepository` |
| EF Config | `{Name}Configuration` | `UserConfiguration` |
| FluentMigrator Migration | `{Version}_{Description}` | `001_CreateUsersTable` |
| Service Interface | `I{Name}Service` | `IEmailService` |
| Service Implementation | `{Name}Service` | `EmailService` |

### Controllers
- Always plural noun (`UsersController`, not `UserController`)
- Route: `api/[controller]` → resolves to `api/users`
- Keep controllers **thin** — no business logic, delegate everything to MediatR

### MediatR
- One command/query per file
- One handler per command/query
- Commands mutate state; Queries return data — never mix them

---

## Entity Framework Core

- Use **Fluent API** for all entity configuration — no data annotations on domain entities
- Each entity has its own configuration class implementing `IEntityTypeConfiguration<T>`
- Place all configurations in `FyndeerAPI.Infrastructure/Persistence/Configurations/`
- `DbContext` is named `AppDbContext` and lives in `FyndeerAPI.Infrastructure/Persistence/`
- Never use `DbContext` directly in controllers — always go through repositories or MediatR handlers
- Always reference `DatabaseSchema` constants for table names, column names, and index names in configurations

---

## DatabaseSchema

All table names, column names, and index names are defined in a single static class:

**Location:** `FyndeerAPI.Infrastructure/Persistence/DatabaseSchema.cs`

```csharp
public static class DatabaseSchema
{
    public static class Tables
    {
        public const string Professionals = "Professionals";
    }

    public static class Columns
    {
        public static class General          // columns shared across tables
        {
            public const string Id = "Id";
            public const string CreatedAt = "CreatedAt";
            public const string UpdatedAt = "UpdatedAt";
        }

        public static class Professionals   // table-specific columns
        {
            public const string FirstName = "FirstName";
            public const string Email = "Email";
            // ...
        }
    }

    public static class Indexes
    {
        public static class Professionals
        {
            public const string EmailUnique = "IX_Professionals_Email";
        }
    }
}
```

### Rules
- Every new table gets its own nested class under `Tables`, `Columns`, and `Indexes`
- Columns shared across multiple tables (e.g. `Id`, `CreatedAt`, `UpdatedAt`) go in `Columns.General`
- Table-specific columns go in `Columns.{TableName}`
- Index constants go in `Indexes.{TableName}`
- Both **migrations** and **EF configurations** must use these constants — never hardcode strings
- Use `using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;` at the top of each file for clean access

---

## FluentMigrator

- All migrations live in `FyndeerAPI.Infrastructure/Migrations/`
- Migration version number format: timestamp (`202604260223`, `202604260728`, `202604261821`)
- Always implement both `Up()` and `Down()`
- Never edit an existing migration — always create a new one
- One schema change per migration — keep them small and focused
- Seed data lives in its own dedicated migration file
- Dev-only seed migrations are tagged with `[Tags("Development")]`
- Always use `DatabaseSchema` constants — never hardcode table/column/index names

### Migration Naming

```
202604260001_CreateProfessionalsTable.cs
202604260002_SeedProfessionals.cs
202604270001_AddSpecializationToProfessionals.cs
```

### Migration runs automatically on startup

Migrations are registered via `AddInfrastructure()` in `FyndeerAPI.Infrastructure/DependencyInjection.cs` and run in `Program.cs` on every startup.

---

## MediatR

- Commands and queries live in `FyndeerAPI.Application/`
- Command + handler in the same file. Query + handler + response DTO in the same file.
- Folder structure mirrors features:

```
FyndeerAPI.Application/
├── Users/
│   ├── Commands/
│   │   ├── CreateUserCommand.cs      ← command + handler
│   │   └── DeleteUserCommand.cs      ← command + handler
│   └── Queries/
│       └── GetUserByIdQuery.cs       ← query + handler + UserResponse
```

### Command and Query interfaces

Never implement `IRequest` or `IRequestHandler` directly. Always use the typed wrappers from `Application/Common`:

| Interface | Use for | Wraps |
|---|---|---|
| `ICommand` | command with no return data | `IRequest<ServiceResult>` |
| `ICommand<T>` | command returning a value | `IRequest<ServiceResult<T>>` |
| `IQuery<T>` | query returning a value | `IRequest<ServiceResult<T>>` |
| `ICommandHandler<TCommand>` | handler for `ICommand` | `IRequestHandler<TCommand, ServiceResult>` |
| `ICommandHandler<TCommand, T>` | handler for `ICommand<T>` | `IRequestHandler<TCommand, ServiceResult<T>>` |
| `IQueryHandler<TQuery, T>` | handler for `IQuery<T>` | `IRequestHandler<TQuery, ServiceResult<T>>` |

```csharp
// Command with no return data
public record DeleteCategoryCommand(int Id) : ICommand;
public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand> { ... }

// Command returning a value
public record CreateCategoryCommand(string Slug, string Name) : ICommand<int>;
public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, int> { ... }

// Query
public record GetCategoryBySlugQuery(string Slug) : IQuery<CategoryResponse>;
public class GetCategoryBySlugQueryHandler : IQueryHandler<GetCategoryBySlugQuery, CategoryResponse> { ... }
```

- Commands mutate state; Queries return data — never mix them
- Handler `Handle` method returns `ServiceResult` / `ServiceResult<T>` directly

---

## Serilog

- Serilog is the **only** logging mechanism — never use `Console.WriteLine` for diagnostics
- Configured in `Program.cs` before anything else
- Structured logging always — use properties, not string interpolation

```csharp
// Correct
Log.Information("User {UserId} created successfully", userId);

// Wrong
Log.Information($"User {userId} created successfully");
```

### MediatR Logging Pipeline

All commands and queries are automatically logged via `LoggingBehaviour<TRequest, TResponse>` in `Application/Common/Behaviours/`. Do **not** add per-handler logging — the behaviour covers everything:

| Event | Level | What is logged |
|---|---|---|
| Request received | `Debug` | Request name + full destructured properties `{@Request}` |
| Request succeeded | `Information` | Request name + elapsed ms |
| Request failed (business error) | `Warning` | Request name + elapsed ms + `ErrorType` + message |
| Unhandled exception | `Error` | Request name + elapsed ms + full exception |

The behaviour uses `Stopwatch` for precise timing on every request. Never add a `Stopwatch` manually inside a handler.

### Log Levels
| Level | When to use |
|---|---|
| `Verbose` | Extremely detailed — loop iterations, raw data |
| `Debug` | Diagnostic info — incoming request payloads |
| `Information` | Normal application events — startup, successful operations |
| `Warning` | Business rule failures — not found, conflict |
| `Error` | Unhandled exceptions that need attention |
| `Fatal` | Application cannot continue |

---

## API Conventions

### Controller Structure

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender) => _sender = sender;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetUserByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
```

## API Documentation (Scalar)

- Scalar is used for API reference in **all environments** (dev and production)
- OpenAPI spec is generated via `builder.Services.AddOpenApi()`
- Scalar UI is served at `/scalar/v1`
- OpenAPI JSON is served at `/openapi/v1.json`
- Never gate Scalar behind `IsDevelopment()` — it must be available in production
- Package: `Scalar.AspNetCore`

---

### HTTP Status Codes
| Scenario | Status Code |
|---|---|
| Successful GET | `200 OK` |
| Successful POST (created) | `201 Created` |
| Successful DELETE / no content | `204 No Content` |
| Validation failure | `400 Bad Request` |
| Not authenticated | `401 Unauthorized` |
| Not authorized | `403 Forbidden` |
| Resource not found | `404 Not Found` |
| Server error | `500 Internal Server Error` |

---

## Environment Strategy

| Environment | Database | Secrets |
|---|---|---|
| Local | Azure SQL Edge (Docker) | `appsettings.Development.json` (gitignored) |
| Production | Azure SQL Serverless | Azure Key Vault |

Never commit secrets. `appsettings.Development.json` is always in `.gitignore`.

---

## Git & Branching

```
main          →  production (protected, auto-deploys to Azure)
develop       →  integration branch
feature/*     →  feature development
fix/*         →  bug fixes
```

### Commit Message Format

```
feat: add user registration endpoint
fix: correct email uniqueness validation
chore: update FluentMigrator to latest version
docs: update CLAUDE.md with logging conventions
```

---

## Local Development Setup

### Prerequisites
- Docker Desktop
- .NET 10 SDK

### Start local database

```bash
docker start sql_local
# or if starting fresh:
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=P@ssw0rD" \
  -p 1433:1433 --name sql_local --platform linux/arm64 \
  -v sqlserver_data:/var/opt/mssql \
  -d mcr.microsoft.com/azure-sql-edge
```

### Run the API

```bash
cd FyndeerAPI.Api
dotnet run
```

On startup the app will:
1. Create the database if it doesn't exist (dev only)
2. Run all pending FluentMigrator migrations
3. Apply dev seed data

---

## Things We Never Do

- Never put business logic in controllers
- Never return EF entities directly from the API — always map to DTOs
- Never use `Console.WriteLine` — use Serilog
- Never edit an existing migration — create a new one
- Never commit `appsettings.Development.json` or any file with secrets
- Never use data annotations on domain entities — use EF Fluent API
- Never call `DbContext` directly from controllers
- Never use string interpolation in Serilog calls
- Never create repositories. We will do actual EFCore work in Application layer
