# LocationsMicroService

A .NET microservice for managing location data (e.g., cities, Countries, etc).  
Supports CRUD operations and can be integrated into larger systems.

---

## 🧩 Structure
- **Locations.API** – RESTful API layer  
- **Locations.APP** – Business logic  
- **CORE** – Shared domain models  
- **ServiceDefaults** – Configuration & defaults  
- **AppHost** – Startup and hosting

---

## 🚀 Setup

```bash
git clone https://github.com/SaraZebardast/LocationsMicroService.git
cd LocationsMicroService
dotnet restore
dotnet run --project Locations.API
