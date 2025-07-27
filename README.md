# 📅 Appointix - Appointment & Scheduling System

Appointix is a web-based appointment and schedule management system built with **ASP.NET Core MVC** and **Entity Framework Core**. It supports **role-based access** for Admin and Client users with clean UI and useful features like:

- 📆 Scheduling
- ✅ Booking appointments
- 🔐 Admin panel access
- 🧑 Role-based data visibility
- 🗂 Pagination, filters, modals & more

---

## 🛠️ Tech Stack

- **ASP.NET Core MVC**
- **Entity Framework Core (Code-First + Migrations)**
- **SQL Server / LocalDB**
- **ASP.NET Identity (Authentication & Authorization)**
- **Bootstrap 5 / jQuery / Font Awesome**
- **DataTables.js** for responsive tables with filters & pagination

---

## 🚀 Getting Started

Follow these steps to clone, build, and run the project:

### 1. 📥 Clone the repository

```bash
git clone https://github.com/your-username/appointix.git
cd appointix
```

### 2. 📦 Restore dependencies

```bash
dotnet restore
```

### 3. 🛠 Build the project

```bash
dotnet build
```

### 4. 🔄 Apply EF Core Migrations (Automatic DB setup)

This project uses **automatic database migration**. When you run the app, the database is created and seeded with:

- Default **roles** (Admin, Client)
- Default **Admin User**

📌 **Default Admin Credentials**:

- **Email**: `admin@appointix.com`  
- **Password**: `Admin@123`

If you want to apply migrations manually:

```bash
dotnet ef database update
```

Make sure you have the EF Core CLI tools installed:

```bash
dotnet tool install --global dotnet-ef
```

---

### 5. ▶️ Run the App

```bash
dotnet run
```

Now visit:

- `https://localhost:5001`  
- or `http://localhost:5000`

---

## 🔐 Accessing the Admin Panel

Login using the seeded admin credentials:

- **Email**: `admin@appointix.com`  
- **Password**: `Admin@123`

> You can change these values inside `DbSeeder.cs` file (inside the `Data` folder).

---

## 👥 User Roles & Access Control

| Role     | Module       | Permissions                                 |
|----------|--------------|---------------------------------------------|
| **Client** | Appointment  | ✅ Create, ✅ View Own                      |
|          | Schedule     | ✅ View                                     |
| **Admin**  | Appointment  | ✅ View All, ❌ Cancel                      |
|          | Schedule     | ✅ Create, ✅ Edit, ✅ Delete, ✅ View       |

> Authentication & Authorization is handled via **ASP.NET Core Identity**.

---

## 📝 Assumptions & Notes

- ✅ No need to manually create DB – EF Core handles it.
- 🔑 Role-based login & access control is implemented.
- ⚙️ Update `appsettings.json` if using different DB connection.
- 💻 Works with **SQL Server** or **LocalDB** (as configured).

---

## 🤝 Contributing

Feel free to fork the repository, improve it, and submit pull requests!

---

## 📄 License

This project is licensed under the **MIT License** – meaning you're free to use, modify, and distribute it with attribution.

---

## 🔗 Useful Links

- [ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [Bootstrap 5 Docs](https://getbootstrap.com/)
