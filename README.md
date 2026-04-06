# 🗳️ CodeVote

**CodeVote API** is a RESTful web API built with ASP.NET Core that enables users to register, authenticate using JWT, and submit/vote for project ideas. 

---

## 🔧 Tech Stack

- **Framework:** ASP.NET Core (.NET 8)
- **Authentication:** JWT (JSON Web Token)
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Logging:** Serilog

---

## 📜 Swagger documentation for API endpoints

![CodeVote Architecture](Images/swaggerDoc.png)

---

## 🛠️ Getting Started

1. **Clone the repository**
	- git clone https://github.com/zeleron7/CodeVote
	
2. **Set up the database**
	- Ensure you have SQL Server installed and running
	- Update the connection string in `appsettings.json` (User Secrets works aswell)
	- run "dotnet ef database update" to apply migrations 

3. **Run the application**
	- Register a new user via the `/CodeVote/Auth/Register` endpoint
	- Login via the `/CodeVote/Auth/Login` endpoint to receive a JWT
	- Authorize in the top right corner of Swagger UI with the token

--- 

## Notes

This project was created as a refresher for core concepts and is not intended for production use.

- Authentication and role-based authorization are not fully implemented.
- Never try to implement your own Authentication.
- No more changes will be made to this project, as it serves its purpose as a learning exercise.

