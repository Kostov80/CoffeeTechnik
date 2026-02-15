# CoffeeTechnik
Description

CoffeeTechnik is a web application for managing coffee machines, locations (objects), and service requests. The project demonstrates the fundamentals of ASP.NET Core MVC with Entity Framework Core, including validation, CRUD operations, roles, and dynamic navigation.

Main Features

Machine management (CRUD) with validation: model, serial number, installation date, and object.

Service request management (Montage, Demontage, Emergency, Maintenance).

Roles and access control: Guest, Sales, Technician.

Dynamic navigation bar based on user role.

Responsive design using Bootstrap 5.

Technologies Used

ASP.NET Core MVC (.NET 8)

Entity Framework Core

SQL Server (LocalDB or other)

Bootstrap 5

Razor Pages, Partial Views, Validation Scripts

Setup Instructions

Clone the repository:

git clone <YOUR_REPO_URL>

Navigate to the project folder and restore dependencies:

dotnet restore


Apply migrations and create the database:

dotnet ef database update


Run the project:

dotnet run


Open a browser and navigate to https://localhost:5001 or http://localhost:5000.

Test Accounts
Username	Password	Role
tech	1234	Technician
sales	1234	Sales
Guest	–	Guest (use Guest Login button)
Project Requirements Covered

Models: Machine, ObjectEntity, ServiceRequest

Controllers: MachinesController, ServiceRequestController

CRUD operations for Machine and ServiceRequest

Validation for Machine (required fields, string length, date)

Dynamic navbar and Logout/Guest Login button

