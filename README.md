# 🏛️ Civic Integrity Hub API

This repository hosts the Civic Integrity Hub API, an initiative to empower citizens in reporting and combating public sector corruption.

## Table of Contents

- [🏛️ Civic Integrity Hub API](#️-civic-integrity-hub-api)
  - [Description](#description)
  - [Badges](#badges)
- [🚀 Quick Start](#-quick-start)
  - [Option 1: Run with Docker (Recommended)](#option-1-run-with-docker-recommended)
  - [Option 2: Run Locally (Development)](#option-2-run-locally-development)
- [📁 Project Structure](#-project-structure)
- [⚙️ Configuration](#️-configuration)
  - [Environment Variables](#environment-variables)
  - [Connection Strings](#connection-strings)
- [🔌 API Endpoints](#-api-endpoints)
- [🐳 Docker Commands](#-docker-commands)
- [💻 Development](#-development)
- [🔧 Troubleshooting](#-troubleshooting)
- [🧪 Testing the API](#-testing-the-api)
- [🤝 Contributing](#-contributing)
- [📚 Additional Resources](#-additional-resources)
- [📄 License](#-license)
- [📧 Contact](#-contact)
- [🙏 Acknowledgments](#-acknowledgments)
- [⭐ Support](#-support)

---

## Description

A citizen-focused ASP.NET Core Web API designed to combat public sector corruption by enabling secure reporting, case tracking, and transparency.

## Badges

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED)](https://www.docker.com/)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1)](https://www.mysql.com/)

---

## 🚀 Quick Start

### Option 1: Run with Docker (Recommended)

This is the easiest way to get started. Docker will handle all dependencies including MySQL.

1. **Clone the repository:**
   ```bash
   git clone https://github.com/VMahluza/PublicCorruptionReportingPlatform.git
   cd PublicCorruptionReportingPlatform
   ```

2. **Navigate to the Infrastructure directory:**
   ```bash
   cd src/Infrastructure
   ```

3. **Start the application:**
   ```bash
   docker-compose up --build
   ```

   This will:
   - Build the .NET API Docker image
   - Start MySQL 8.0 database
   - Initialize the database with seed data
   - Start the API on port 5000

4. **Access the application:**
   - **API Root:** http://localhost:5000
   - **Swagger UI:** http://localhost:5000/swagger
   - **Health Check:** http://localhost:5000/api/health
   - **Connection Test:** http://localhost:5000/api/health/connection

5. **Stop the application:**
   ```bash
   # Stop services
   docker-compose down

   # Stop and remove all data (volumes)
   docker-compose down -v
   ```

---

### Option 2: Run Locally (Development)

For local development without Docker:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/VMahluza/PublicCorruptionReportingPlatform.git
   cd PublicCorruptionReportingPlatform
   ```

2. **Set up MySQL database:**

   Make sure MySQL is running locally on port 3306, or start it with Docker:
   ```bash
   cd src/Infrastructure
   docker-compose up mysql -d
   ```

3. **Configure User Secrets (for sensitive data):**
   ```bash
   cd src/Presentation
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=civic_integrity_hub_db;User=Victor;Password=12345StrongPwd!;"
   ```

4. **Restore dependencies:**
   ```bash
   cd ../..
   dotnet restore
   ```

5. **Run the application:**
   ```bash
   dotnet run --project src/Presentation/Presentation.csproj
   ```

6. **Access the application:**
   - **API:** https://localhost:7299 or http://localhost:5283
   - **Swagger UI:** https://localhost:7299/swagger

---

## 📁 Project Structure

```
PublicCorruptionReportingPlatform/
├── src/
│   ├── Presentation/           # ASP.NET Core Web API (Entry Point)
│   │   ├── Controllers/        # API Controllers
│   │   │   ├── HealthController.cs
│   │   │   └── WeatherForecastController.cs
│   │   ├── Program.cs          # Application startup
│   │   ├── appsettings.json    # Production configuration
│   │   ├── appsettings.Development.json
│   │   └── Dockerfile          # Docker configuration
│   │
│   ├── Application/            # Business logic layer
│   ├── Domain/                 # Domain models & entities
│   ├── Infrastructure/         # Data access & external services
│   │   ├── docker-compose.yml  # Docker orchestration
│   │   ├── .env                # Environment variables
│   │   └── initdb/             # Database initialization scripts
│   │       └── 001_seed.sql
│   │
│   └── Tests/                  # Unit & integration tests
│
├── ProblemStatement.md         # Project overview & roadmap
└── README.md                   # This file
```

---

## ⚙️ Configuration

### Environment Variables

The application uses environment variables for configuration. These are defined in `src/Infrastructure/.env`:

```env
MYSQL_ROOT_PASSWORD=12*****wd!
MYSQL_DATABASE=civi******_db
MYSQL_USER=Victor
MYSQL_PASSWORD=12*******wd!
MYSQL_PORT=3306
```

🔒 **Security Note:** 
- The `.env` file is gitignored and should never be committed to version control
- Use proper secret management in production (Azure Key Vault, AWS Secrets Manager, etc.)
- Change default passwords before deploying to production

### Connection Strings

**Development (Local):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=ci*****y_hub_db;User=Victor;Password=12******wd!;"
  }
}
```

**Production (Docker):**
Environment variable override:
```
ConnectionStrings__DefaultConnection=Server=mysql;Port=3306;Database=civic_*****db;User=Victor;Password=123******gPwd!;
```

---

## 🔌 API Endpoints

### Root Endpoint
```http
GET /
```
Returns API information and available endpoints.

**Response:**
```json
{
  "service": "Civic Integrity Hub API",
  "version": "1.0.0",
  "status": "Running",
  "endpoints": [
    "/swagger",
    "/api/health",
    "/api/health/connection"
  ]
}
```

### Health Check
```http
GET /api/health
```
Returns API health status and environment information.

**Response:**
```json
{
  "status": "Healthy",
  "environment": "Production",
  "timestamp": "2025-01-03T12:00:00Z",
  "message": "Civic Integrity Hub API is running"
}
```

### Connection Test
```http
GET /api/health/connection
```
Returns masked connection string for troubleshooting (password hidden).

**Response:**
```json
{
  "connectionString": "Server=mysql;Port=3306;Database=civi*****ub_db;User=Victor;Password=****;",
  "environment": "Production"
}
```

### Weather Forecast (Sample)
```http
GET /weatherforecast
```
Sample endpoint demonstrating API functionality.

### API Documentation
Visit `/swagger` for interactive API documentation powered by Swagger/OpenAPI.

---

## 🐳 Docker Commands

### Build and Start
```bash
# Build and start all services
docker-compose up --build

# Start in detached mode (background)
docker-compose up -d

# Start only MySQL
docker-compose up mysql -d

# Rebuild without cache
docker-compose build --no-cache
docker-compose up
```

### Stop and Clean Up
```bash
# Stop all services
docker-compose down

# Stop and remove volumes (deletes database data)
docker-compose down -v

# Remove all unused containers, networks, images
docker system prune -a
```

### View Logs
```bash
# View all logs
docker-compose logs

# View API logs only
docker logs civic_integrity_hub_api

# View MySQL logs only
docker logs mysql8-civic_integrity_hub_db

# Follow logs in real-time
docker-compose logs -f

# View last 100 lines
docker-compose logs --tail=100
```

### Access Containers
```bash
# Access API container bash
docker exec -it civic_integrity_hub_api /bin/bash

# Access MySQL container
docker exec -it mysql8-civic_integrity_hub_db mysql -u Victor -p

# View environment variables in API container
docker exec civic_integrity_hub_api printenv

# Check MySQL status
docker exec mysql8-civic_integrity_hub_db mysqladmin -u Victor -p status
```

### Restart Services
```bash
# Restart all services
docker-compose restart

# Restart only API
docker-compose restart api

# Restart only MySQL
docker-compose restart mysql
```

---

## 💻 Development

### Running Tests
```bash
cd src/Tests
dotnet test

# With detailed output
dotnet test --verbosity normal

# Generate code coverage
dotnet test /p:CollectCoverage=true
```

### Building the Solution
```bash
# Build all projects
dotnet build

# Build in Release mode
dotnet build -c Release

# Clean build artifacts
dotnet clean
```

### Database Migrations (Future)
```bash
# Add a new migration
dotnet ef migrations add InitialCreate --project src/Infrastructure --startup-project src/Presentation

# Update database
dotnet ef database update --project src/Infrastructure --startup-project src/Presentation

# Remove last migration
dotnet ef migrations remove --project src/Infrastructure --startup-project src/Presentation
```

### Code Formatting
```bash
# Format code
dotnet format

# Check formatting without changes
dotnet format --verify-no-changes
```

### Building for Production
```bash
dotnet publish src/Presentation/Presentation.csproj -c Release -o ./publish
```

---

## 🔧 Troubleshooting

### Issue: Port Already in Use

**Problem:** `Error: bind: address already in use`

**Solution:**
```bash
# Windows - Check what's using port 5000
netstat -ano | findstr :5000

# Linux/Mac - Check what's using port 5000
lsof -i :5000

# Kill the process or change the port in docker-compose.yml
ports:
  - "5001:80"  # Use port 5001 instead of 5000
```

### Issue: Database Connection Failed

**Problem:** API can't connect to MySQL

**Solutions:**
```bash
# 1. Check if MySQL container is running
docker ps

# 2. Check MySQL logs for errors
docker logs mysql8-civic_integrity_hub_db

# 3. Verify MySQL is healthy
docker exec mysql8-civic_integrity_hub_db mysqladmin ping -h localhost

# 4. Check environment variables in API container
docker exec civic_integrity_hub_api printenv | grep ConnectionStrings

# 5. Restart MySQL container
docker-compose restart mysql

# 6. Wait for MySQL to be ready (it takes ~10 seconds on first run)
docker-compose logs mysql | grep "ready for connections"
```

### Issue: Container Won't Start

**Problem:** Docker container fails to start or crashes

**Solutions:**
```bash
# 1. View container logs
docker logs civic_integrity_hub_api

# 2. Remove old containers and rebuild
docker-compose down -v
docker system prune -a

# 3. Rebuild from scratch
docker-compose up --build --force-recreate

# 4. Check Dockerfile syntax
docker build -t test-build -f src/Presentation/Dockerfile .
```

### Issue: Changes Not Reflected

**Problem:** Code changes not showing up in Docker container

**Solution:**
```bash
# Rebuild the containers
docker-compose down
docker-compose up --build

# Force rebuild without cache
docker-compose build --no-cache
docker-compose up
```

### Issue: "version is obsolete" Warning

**Problem:** `version: "3.9"` warning in docker-compose

**Solution:**
This is just a warning in Docker Compose v2+. You can safely ignore it or remove the `version` line from `docker-compose.yml`.

### Issue: MySQL "Connection Refused"

**Problem:** Can't connect to MySQL from host machine

**Solutions:**
```bash
# 1. Wait for MySQL to fully start (check healthcheck)
docker-compose logs mysql | grep "ready for connections"

# 2. Verify port mapping
docker ps | grep mysql

# 3. Test connection from host
mysql -h 127.0.0.1 -P 3306 -u Victor -p

# 4. Test connection from API container
docker exec civic_integrity_hub_api ping mysql
```

---

## 🧪 Testing the API

### Using cURL
```bash
# Health check
curl http://localhost:5000/api/health

# Connection test
curl http://localhost:5000/api/health/connection

# Root endpoint
curl http://localhost:5000/

# Weather forecast
curl http://localhost:5000/weatherforecast
```

### Using PowerShell
```powershell
# Health check
Invoke-WebRequest -Uri http://localhost:5000/api/health | Select-Object -Expand Content

# Connection test
Invoke-RestMethod -Uri http://localhost:5000/api/health/connection
```

### Using Swagger UI
1. Open http://localhost:5000/swagger
2. Expand any endpoint
3. Click "Try it out"
4. Click "Execute"

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards
- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Write unit tests for new features
- Keep controllers thin, business logic in services

---

## 📚 Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Docker Documentation](https://docs.docker.com/)
- [MySQL Documentation](https://dev.mysql.com/doc/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 📧 Contact

**Victor Mahluza** - [@VMahluza](https://github.com/VMahluza)

**Project Link:** [https://github.com/VMahluza/PublicCorruptionReportingPlatform](https://github.com/VMahluza/PublicCorruptionReportingPlatform)

---

## 🙏 Acknowledgments

- Inspired by South Africa's anti-corruption initiatives
- [Corruption Watch South Africa](https://www.corruptionwatch.org.za)
- President Cyril Ramaphosa's 2025 remarks on transparency
- NACAC recommendations on AI and data-sharing in anti-corruption systems

---

## ⭐ Support

If you find this project helpful, please consider giving it a star on GitHub!

**Built with ❤️ for transparency and accountability in South Africa**
