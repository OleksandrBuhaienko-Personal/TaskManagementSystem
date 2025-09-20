# TaskManagementSystem

TaskManagementSystem is a modular ASP.NET Core solution targeting .NET 8. It implements Clean Architecture with clear separation of concerns, supports OData for rich querying, and uses JWT for authentication with role-based authorization.

## Contents
- [Overview](#overview)
- [Solution structure](#solution-structure)
- [Clean Architecture](#clean-architecture)
- [OData support](#odata-support)


## Overview
TaskManagementSystem is designed for maintainability and scalability:
- Clean Architecture separates domain logic from application services and infrastructure.
- Modularity keeps features isolated, discoverable, and independently evolvable.
- OData provides advanced server-side querying (filtering, sorting, paging, projections, and expansions).
- Security is handled with JWT Bearer authentication and role-based authorization.

## Solution structure
- src/
  - Domain/ — Core domain models, value objects, invariants, domain events, and domain-specific interfaces.
  - Application/
    - TaskManagementSystem.Auth/ — Authentication services and JWT helpers.
    - TaskManagementSystem.Application/ — Use cases, orchestrators, and application contracts.
  - Infrastructure/ — Persistence, repositories, identity integration.
  - TaskManagementSystem.WebAPI/ — HTTP endpoints (REST and OData), DI composition root, middleware, and configuration.

## Clean Architecture
The solution follows Clean Architecture principles:
- Domain (enterprise business rules)
  - Entities, value objects, domain services, domain events, aggregates, and specifications.
  - Zero external dependencies: avoid coupling to frameworks, ORMs, or UI concerns.
  - Encapsulates invariants and business rules; changes here are the most stable and well-tested.
- Application (application business rules)
  - Coordinates use cases, transaction boundaries, and business workflows.
  - Contracts (interfaces) define required capabilities (persistence, identity, emailing, etc.).
  - Cross-cutting: mapping, policies, and behaviors.
- Infrastructure (implementation details)
  - Implements contracts from Application (e.g., repositories, identity, cache).
  - Contains database contexts, migrations, email/sms gateways, and external service clients.
  - Replaceable without changing Domain/Application.
- Web API (presentation)
  - Composition root: configures DI, authentication/authorization, OData, versioning, and middleware.
  - Controllers/Endpoints are thin: orchestration only, no core business logic.

Dependency rules:
- WebAPI → Application → Domain
- WebAPI → Infrastructure (composition only)
- Domain has no dependency on outer layers.

Benefits:
- Testability, replaceability, maintainability, and clearer onboarding for new contributors.

## OData support
The API supports OData for rich querying over entities:
- Core query options:
  - $filter: server-side filtering (e.g., status eq 'Open' and priority gt 2).
  - $select: projection to return only required fields.
  - $orderby: sorting by field(s), ascending or descending.
  - $top / $skip: paging (combine with $count=true to get total items).
  - $expand: include related entities (e.g., assignee, comments).

Example requests (adjust base URL/routes): {{baseApi}}/api/user?$filter=FirstName eq 'John'&$top=10&$skip=0
