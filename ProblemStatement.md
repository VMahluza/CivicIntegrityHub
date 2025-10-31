# Public Corruption Reporting Platform API

A citizen-focused ASP.NET Web API designed to combat public sector corruption by enabling secure reporting, case tracking, and transparency.

---

## ✨ Project Overview

**Civic Integrity Hub** empowers the South African public to report and monitor corruption incidents involving government entities. Built with ASP.NET Core Web API, it is designed to evolve into a scalable, enterprise-grade system with support for transparency dashboards, analytics, secure submissions, and cloud deployment.

---

## 🚫 The Problem

Corruption in South Africa undermines public trust and weakens service delivery. While platforms like Corruption Watch exist, there's still a lack of:

* Scalable citizen-facing infrastructure for real-time corruption reporting.
* Transparent tracking of reported cases.
* Public dashboards to hold authorities accountable.
* Integration with investigative and oversight workflows.

---

## 🚀 The Solution

**Civic Integrity Hub** provides:

### Core Features (MVP):

* **Secure corruption report submission**
* **Role-based case management** (Citizen, Investigator, Admin)
* **Evidence upload** (files, images)
* **Filtering & querying reports** (by department, region, category)
* **Public dashboard** (aggregated, anonymized stats)
* **Audit trail & status updates**

### Advanced Features (Future Iterations):

* **CQRS + MediatR** for separation of reads/writes
* **Messaging integration** (RabbitMQ or Azure Service Bus)
* **Notification service** (email/SMS on status change)
* **Open data APIs** for media/researchers
* **AI-based flagging of suspicious patterns**
* **CI/CD deployment via GitHub Actions + cloud hosting**

---

## 🔄 Development Roadmap

### Week 1: Fundamentals (CRUD API)

* [ ] Entity models: User, Report, Department, Category
* [ ] SQLite/SQL Server + EF Core setup
* [ ] CRUD endpoints for reports
* [ ] Swagger + AutoMapper + DTO validation
* [ ] Evidence file upload (IFormFile)

### Week 2: Intermediate (Auth + Structure)

* [ ] JWT authentication + roles
* [ ] Clean Architecture refactor
* [ ] In-memory caching for public stats
* [ ] Advanced LINQ filtering
* [ ] Data annotations validation

### Week 3: Advanced (CQRS + Testing)

* [ ] MediatR + CQRS structure
* [ ] Domain-Driven aggregates
* [ ] Dapper read-optimization (optional)
* [ ] Unit + integration tests
* [ ] Auditing middleware (CreatedBy, UpdatedAt)
* [ ] API versioning support

### Week 4: Enterprise Integration

* [ ] Microservices separation (Notifications, Analytics)
* [ ] RabbitMQ integration for async event handling
* [ ] Background jobs (Hangfire)
* [ ] Distributed caching (Redis)
* [ ] Rate limiting + concurrency controls

### Week 5: Production Readiness

* [ ] Docker containerization
* [ ] Cloud deployment (Azure App Service or AWS Elastic Beanstalk)
* [ ] CI/CD pipelines via GitHub Actions
* [ ] Secret management + environment configs
* [ ] Logging (Serilog + Seq or AppInsights)
* [ ] API client documentation + Postman collection

---

## 🌐 Target Users

* **General Public / Whistleblowers**
* **Journalists & Researchers**
* **Investigative Bodies (e.g. SIU, NPA)**
* **Government Oversight Agencies**

---

## 📈 Potential Impact

* Increased public engagement in anti-corruption efforts.
* Transparent monitoring of government service abuse.
* Better data insights for policy-makers and watchdogs.
* A model open-data platform aligned with global anti-corruption goals.

---

## 📄 References

* President Cyril Ramaphosa's 2025 remarks on corruption & transparency
* Corruption Watch South Africa: [https://www.corruptionwatch.org.za](https://www.corruptionwatch.org.za)
* NACAC recommendations: AI and data-sharing in anti-corruption systems

---

## 💡 Let's Build It

> This project is designed for weekly iteration. Each phase adds production-level capabilities, improving both technical skills and public value.

**Start with Week 1: Build a simple CRUD API to report and list corruption cases.**
