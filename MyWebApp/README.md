```ps1
# run from WebApi project
dotnet ef migrations add InitialCreate --project ../Infrastructure --startup-project .
```
- Architecture Overview
```
Frontend (React + TypeScript)
    ↓
API Layer (FastEndpoints)
    ↓
Application Layer (Business Logic)
    ↓
Domain Layer (Core Business Rules)
    ↓
Infrastructure Layer (Data Access)
```

# Backend
- Sqllite + EF Core + Migrations
A. Policy Pattern for Business Rules: Instead of hardcoding business rules, I created a composable validation system. Each rule is a separate class that can be tested independently
B. Value Objects for Type Safety: to avoid primitive obsesión aim to work with 'always valid' value objects

# Frontend
- React + typescript
A. Zod validation: Runtime type safety
B. React Hook Form: input forms

# Testing
- Basic unit test for logic