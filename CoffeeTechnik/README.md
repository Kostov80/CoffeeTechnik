# ☕ CoffeeTechnik - ASP.NET Advanced Project

## 📌 Description
CoffeeTechnik is a web application for managing coffee machines, inventory parts, objects (locations), and service requests.  
The system demonstrates ASP.NET Core MVC architecture with Entity Framework Core, Identity authentication, roles, and layered service design.

---

## ⚙ Technologies Used
- ASP.NET Core MVC (.NET 6+)
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- Bootstrap 5
- Razor Views

---

## 🧩 Main Features

### ☕ Coffee Machines
- CRUD operations (Create, Read, Update, Delete)
- Assign machines to objects
- Track installation date and serial number

### 📦 Inventory System
- Manage spare parts
- Add, edit, delete parts
- Search functionality

### 🏢 Objects
- Manage client locations
- Link machines to objects

### 🛠 Service Requests
- Create and manage service requests
- Track request status (Maintenance, Repair, etc.)

### 👤 Authentication & Roles
- ASP.NET Identity system
- Roles: Admin, Technician, Sales
- Role-based access control

---

## 🏗 Architecture
- MVC Pattern
- Service Layer (Business Logic)
- Dependency Injection
- Separation of concerns (Controllers / Services / Data layer)

---

## 🗄 Database
Main entities:
- CoffeeMachine
- InventoryItem
- ObjectEntity
- ServiceRequest
- ServiceStatus
- Identity Users & Roles

---

## 🌱 Seed Data
The application seeds:
- Default roles: Admin, Technician, Sales
- Admin user account
- Sample coffee machines and objects

---

## 🚀 How to Run

1. Clone repository
2. Update connection string in `appsettings.json`
3. Run migrations:
```bash
Update-Database

4. Start application:
```bash
dotnet run

5.Open browser:
https://localhost:5001

---

## 🔐 Security
- ASP.NET Identity authentication
- Role-based authorization
- AntiForgeryToken protection
- Validation on all forms
- EF Core prevents SQL injection

---

## 📊 Notes
- Built with ASP.NET Core MVC architecture
- Uses service layer pattern
- Includes validation and error handling pages (404 / 500)
- Responsive UI with Bootstrap

---

## 👨‍🎓 Author
Student project – ASP.NET Advanced Course