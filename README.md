# 📦 Inventory Hub

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet)\
![Blazor](https://img.shields.io/badge/Blazor-Server-blue?logo=blazor)\
![SQL
Server](https://img.shields.io/badge/Database-SQL%20Server-red?logo=microsoftsqlserver)\
![License](https://img.shields.io/badge/License-MIT-green)\
![Status](https://img.shields.io/badge/Status-Active-success)

**Inventory Hub** is a full-stack **.NET Blazor Server** application
designed to manage IT assets and resources in organizations. It provides
complete lifecycle management for devices such as servers, switches,
firewalls, laptops, desktops, cables, and more. The system helps IT
admins and users track, assign, maintain, and export reports of all
assets with role-based access and digital documentation.

------------------------------------------------------------------------

## 🚀 Features

-   **Asset Management**
    -   Add, update, and delete IT devices (servers, switches,
        firewalls, laptops, desktops, cables, etc.)\
    -   Manage asset lifecycle: in stock, assigned, maintenance,
        end-of-life
-   **Assignments**
    -   Assign devices to users with full tracking\
    -   Generate printable assignment documents with digital signatures
        (User, IT Agent, and Admin)\
    -   Track history of all assignments
-   **Maintenance**
    -   Create and manage maintenance records for every device\
    -   View complete history of maintenance logs\
    -   Track service dates, issues, resolutions, and assigned
        technicians
-   **Reports & Exports**
    -   Export asset data and maintenance logs in **Excel** and **PDF**
        formats\
    -   Filter and search across all devices and records
-   **Authorization & Roles**
    -   Role-based access (Admin, Agent, User)\
    -   Secure authentication and authorization using ASP.NET Core
        Identity
-   **Usability Features**
    -   Filtering and sorting across devices and logs\
    -   Dashboard with insights about assets and statuses\
    -   Notifications for maintenance schedules and assignments

------------------------------------------------------------------------

## 🛠️ Tech Stack

-   **Frontend & Backend**: Blazor Server (.NET 8)\
-   **Database**: Microsoft SQL Server (EF Core ORM)\
-   **Authentication**: ASP.NET Core Identity with role-based access\
-   **Exporting**: ClosedXML (Excel), iText7 / QuestPDF (PDFs)\
-   **Real-time Updates**: SignalR (for notifications)

------------------------------------------------------------------------


## ⚙️ Installation & Setup

1.  **Clone Repository**

    ``` bash
    git clone https://github.com/Abdelrahman2264/InventoryHub.git
    cd InventoryHub
    ```

2.  **Configure Database**

    -   Update the connection string in `appsettings.json`\

    -   Run migrations:

        ``` bash
        dotnet ef database update
        ```

3.  **Run Application**

    ``` bash
    dotnet run
    ```

4.  **Access App**

    -   Open browser: `https://localhost:5001`

------------------------------------------------------------------------

## 👥 User Roles

-   **Admin**
    -   Full access: manage assets, users, maintenance, and reports\
-   **Agent**
    -   Manage asset assignments, maintenance logs, and generate
        documents\
-   **User**
    -   View assigned assets and sign documents

------------------------------------------------------------------------

## 📊 Example Use Cases

-   IT Admin registers a new laptop in the system → assigns it to a user
    → prints an assignment document → user signs.\
-   Technician adds a maintenance record for a firewall → system tracks
    history → Admin exports maintenance logs in Excel/PDF.\
-   Manager views dashboard of assets in stock, assigned devices, and
    maintenance schedules.

------------------------------------------------------------------------

## 📜 License

This project is licensed under the **MIT License**.
