# EfCoreCryptoValueConverters

A demonstration project showcasing automatic encryption/decryption of sensitive data in Entity Framework Core using Value Converters with AES encryption, following Clean Architecture principles.

## Project Overview

This project demonstrates how to transparently encrypt and decrypt sensitive database fields using EF Core's Value Converters. When an entity property is marked with the `[Encrypted]` attribute, it is automatically encrypted before being stored in the database and decrypted when retrieved.

## Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:

```
├── Core/
│   ├── Domain/              # Enterprise business rules
│   │   ├── Entities/        # Domain entities
│   │   ├── Attributes/      # Custom attributes ([Encrypted])
│   │   ├── DTOs/            # Data Transfer Objects
│   │   └── Enums/           # Domain enumerations
│   └── Application/         # Application business rules
│       ├── Managers/        # Business logic managers
│       ├── Repositories/    # Repository interfaces
│       └── DependencyRegistration/
├── Infrastructure/
│   └── Persistence/         # Data access layer
│       ├── Contexts/        # DbContext and factory
│       ├── Repositories/    # Repository implementations
│       ├── Security/        # Encryption converters & helpers
│       └── Configurations/  # Database configuration
└── Presentation/
    └── Api/                 # RESTful API endpoints
        └── Controllers/     # API controllers
```

## Key Features

### Automatic Encryption

- Properties marked with `[Encrypted]` attribute are automatically encrypted/decrypted
- Uses AES-256 encryption in CBC mode
- Transparent to application code - no manual encryption/decryption needed

### Clean Architecture

- Domain-driven design
- Dependency inversion principle
- Separation of concerns across layers

### Repository Pattern

- Generic base repositories for CRUD operations
- Strongly-typed, reusable data access layer

### Value Converters

- EF Core Value Converters for seamless data transformation
- Smart detection to avoid double encryption
- Base64 encoding validation

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- PostgreSQL 12+
- IDE (Visual Studio, Rider, or VS Code)

### Database Setup

1. Update connection string in `Presentation/Api/appsettings.json`:

```json
"ConnectionStrings": {
  "Psql": "Host=localhost;Port=5432;Database=EfCoreCryptoDb;Username=your_user;Password=your_password"
}
```

2. Run migrations:

```bash
cd Infrastructure/Persistence
dotnet ef database update
```

### Running the Application

```bash
cd Presentation/Api
dotnet run
```

The API will be available at `https://localhost:5001` (or the port specified in launchSettings.json)

## API Endpoints

### Users

| Method | Endpoint          | Description          |
| ------ | ----------------- | -------------------- |
| POST   | `/api/users`      | Create a new user    |
| GET    | `/api/users/{id}` | Get user by ID       |
| GET    | `/api/users`      | Get all users        |
| PUT    | `/api/users`      | Update existing user |
| DELETE | `/api/users/{id}` | Delete user          |

### Sample Request (Create User)

```json
POST /api/users
{
  "name": "John",
  "surname": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "+1234567890"
}
```

**Note:** `email` and `phoneNumber` will be automatically encrypted in the database!

## How Encryption Works

### 1. Mark Properties for Encryption

```csharp
public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }

    [Encrypted] // This property will be encrypted
    public string Email { get; set; }

    [Encrypted] // This property will be encrypted
    public string PhoneNumber { get; set; }
}
```

### 2. Automatic Converter Application

The `ModelBuilderEncryptionExtensions` scans all entities for `[Encrypted]` attributes and applies the `AesEncryptedConverter`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyEncryptedProperties(); // Applies converters automatically
}
```

### 3. Transparent Encryption/Decryption

- **Writing to DB:** Plain text → AES Encryption → Base64 → Database
- **Reading from DB:** Database → Base64 → AES Decryption → Plain text

### 4. Smart Detection

The converter checks if data is already encrypted (Base64 validation) to prevent double encryption:

```csharp
private static bool LooksEncrypted(string? value)
{
    // Validates Base64 format to detect already encrypted data
}
```

## Security Considerations

**Important:** This is a demonstration project. For production use:

1. **Never hardcode encryption keys** - Use Azure Key Vault, AWS Secrets Manager, or environment variables
2. **Use unique IV (Initialization Vector)** for each encryption operation
3. **Implement key rotation** strategy
4. **Use authenticated encryption** (AES-GCM instead of AES-CBC)
5. **Secure connection strings** in production
6. **Add proper logging and monitoring**
7. **Implement data validation and sanitization**

## Project Structure Details

### Domain Layer

- **Entities:** Core business objects (`User`, `BaseEntity`)
- **Attributes:** Custom attributes for metadata (`EncryptedAttribute`)
- **DTOs:** Request/Response models for API communication
- **Enums:** Domain enumerations (`Status`)

### Application Layer

- **Managers:** Business logic and orchestration (`UserManager`)
- **Repository Interfaces:** Contracts for data access
- **Service Registration:** Dependency injection configuration

### Infrastructure Layer

- **Repositories:** Concrete implementations of repository interfaces
- **Security:** Encryption converters and helpers
- **DbContext:** Entity Framework database context
- **Migrations:** Database schema versioning

### Presentation Layer

- **Controllers:** RESTful API endpoints
- **Configuration:** Application settings and startup

## Testing the Encryption

1. Create a user via API:

```bash
curl -X POST https://localhost:5001/api/users \
  -H "Content-Type: application/json" \
  -d '{"name":"Jane","surname":"Doe","email":"jane@example.com","phoneNumber":"+9876543210"}'
```

2. Check the database - `email` and `phoneNumber` will be stored as encrypted Base64 strings

3. Retrieve the user via API - data will be automatically decrypted:

```bash
curl http://localhost:5282/api/users/{id}
```

## 🔧 Technologies Used

- **.NET 9.0**
- **Entity Framework Core 9.0**
- **PostgreSQL** with Npgsql provider
- **ASP.NET Core Web API**
- **AES Encryption** (System.Security.Cryptography)
