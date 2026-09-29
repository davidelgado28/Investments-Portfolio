# Investments Portfolio API 

A robust, enterprise-grade **.NET 8 Web API** designed for managing investment portfolios (Stocks & Cryptocurrencies). This project demonstrates intermediate-to-advanced enterprise software development patterns, focusing on **Clean Architecture**, rich domain models, and efficient data handling with Entity Framework Core and advanced LINQ.

---

## Architecture & Project Structure

The project follows the **Clean Architecture** principles to ensure high maintainability, testability, and separation of concerns across layers:

```text
InvestmentsPortfolio/
│
├── src/
│   ├── Investments.Domain/             # Pure business rules, Entities, Enums, and Value Objects
│   │   ├── Entities/
│   │   │   └── Transacao.cs
│   │   └── Enums/
│   │       └── TipoTransacao.cs
│   │
│   ├── Investments.Application/        # Use cases, DTOs, interfaces, and orchestration logic
│   │   ├── DTOs/
│   │   │   └── CriarTransacaoDto.cs
│   │   └── Interfaces/                 # Repositories & Domain Services abstractions
│   │
│   ├── Investments.Infrastructure/     # Persistence (EF Core), Migrations, and Concrete Repositories
│   │   ├── Context/
│   │   │   └── AppDbContext.cs
│   │   └── Configurations/             # EF Core Fluent API mappings
│   │
│   └── Investments.Api/                # Presentation layer (Minimal APIs / Controllers)
│       ├── Endpoints/
│       │   └── TransacaoEndpoints.cs
│       └── Program.cs
│
└── tests/
    └── Investments.Domain.Tests/       # Unit tests (xUnit) focusing on domain logic
```

---

## Tech Stack & Highlights

- **Framework:** .NET 8 (C# 12)
- **Database Access:** Entity Framework Core with Fluent API configurations, precise decimal handling (`HasPrecision(18, 8)` for crypto fractions), and composite indexing.
- **API Style:** Minimal APIs for optimal performance and clean routing.
- **Business Logic:** Chronological weighted average price (WAP) calculation handling both purchases and sales.

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine.

### Installation & Running

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/InvestmentsPortfolio.git
   cd InvestmentsPortfolio
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the API:
   ```bash
   dotnet run --project src/Investments.Api/Investments.Api.csproj
   ```

---

## API Endpoints Example

### Register Transaction & Calculate Weighted Average Price
- **POST** `/api/transacoes`
- **Payload Example:**
  ```json
  {
    "ativo": "BTC",
    "quantidade": 0.5,
    "precoCompra": 45000.00,
    "dataTransacao": "2026-03-30T10:00:00Z",
    "tipo": 1,
    "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
  ```

---

## roadmap & Enterprise Patterns

To demonstrate enterprise readiness, future iterations of this repository will include:
- **Repository & Unit of Work Pattern:** Decoupling EF Core context from endpoints to enable robust mocking and unit testing.
- **CQRS with MediatR:** Separating write commands (`CriarTransacaoCommand`) from read queries (`ObterExtratoPortfolioQuery`).
- **Domain-Driven Design (DDD) Value Objects:** Enforcing encapsulation and intrinsic validations (e.g., non-negative quantities).

---

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
