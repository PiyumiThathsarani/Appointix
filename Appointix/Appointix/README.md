cat > README.md << EOF
# Appointix - Appointment and Scheduling Management System

**Appointix** is a web-based appointment scheduling application built with ASP.NET Core MVC. It allows **clients** to create and manage their appointments while enabling **admins** to manage schedules, view all appointments, and perform administrative actions like canceling or editing schedules.

---

## 🛠 Tech Stack

- **.NET 6 / ASP.NET Core MVC** – Backend framework
- **Entity Framework Core** – ORM for database interactions
- **SQL Server** – Relational database system
- **Bootstrap 5** – Front-end styling
- **jQuery & AJAX** – Interactive UI and modal handling
- **Identity** – Role-based access (Admin / Client)

---

## 🚀 Features

### Admin Panel
- View all appointments
- Cancel appointments
- Add, edit, delete schedules

### Client Panel
- Book appointments
- View their own appointments
- View available schedules

### Other Features
- Role-based authorization
- Pagination and filtering
- Confirmation modals for actions
- Eye-catching and responsive UI

---

## ⚙️ Getting Started

Follow these steps to run the project on your local machine.

### 1. 📥 Clone the repository

\`\`\`bash
git clone https://github.com/PiyumiThathsarani/Appointix
cd Appointix
\`\`\`

### 2. 📦 Restore dependencies

\`\`\`bash
dotnet restore
\`\`\`

### 3. 🛠 Build the project

\`\`\`bash
dotnet build
\`\`\`

### 4. 🔄 Apply EF Core Migrations (Automatic DB setup)

This project uses **automatic database migration**. When you run the app, the database is created and seeded with default roles and an admin user.

> 📌 **Default Admin User**
> - **Email:** \`admin@appointix.com\`
> - **Password:** \`Admin@123\`

If you want to apply migrations manually:

\`\`\`bash
dotnet ef database update
\`\`\`

> Make sure you have the \`dotnet-ef\` tool installed:
> \`\`\`bash
> dotnet tool install --global dotnet-ef
> \`\`\`

### 5. ▶️ Run the app

\`\`\`bash
dotnet run
\`\`\`

Navigate to \`https://localhost:5001\` or \`http://localhost:5000\`

---

## 🔐 Accessing the Admin Panel

Login with the seeded admin credentials:

\`\`\`
Email: admin@appointix.com
Password: Admin@123
\`\`\`

> You can change these values in the \`DbSeeder.cs\` file (inside Data folder) if needed.

---

## 📝 Assumptions and Notes

- No need to manually create a database. EF Core migrations will handle it.
- Role-based access (Admin/Client) is already implemented.
- Admin can access all views; clients can only manage their own data.
- Uses local database connection string in \`appsettings.json\`. Update it if needed.
- Make sure your machine has SQL Server or use LocalDB as configured.

---

## 🤝 Contributing

Feel free to fork the repo and submit pull requests.

---

## 📄 License

This project is licensed under the MIT License.

---
EOF
