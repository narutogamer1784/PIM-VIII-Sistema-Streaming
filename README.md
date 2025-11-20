Multimedia Streaming System (PIM VIII)

A cross-platform multimedia streaming solution developed as part of the Multidisciplinary Integrated Project (PIM VIII). This ecosystem integrates a .NET Web API, a .NET MAUI Desktop App for content creators, and a Native Android App for end-users.

Project Overview

The system is designed to manage the lifecycle of multimedia content:
1.  **Creators** upload videos/music and manage playlists via a **Windows Desktop App**.
2.  **The Backend** processes data and stores it in a relational database.
3.  **Consumers** browse and view content details via an **Android Mobile App**.

Tech Stack

1. Backend (The Engine)
* **Framework:** .NET 8 / ASP.NET Core Web API
* **Language:** C#
* **Database:** SQLite (via Entity Framework Core)
* **Architecture:** RESTful API, Repository Pattern, DTOs.
* **Key Features:**
    * User Authentication (Login).
    * CRUD operations for Playlists and Contents.
    * Complex relationship management (Many-to-Many).

2. Desktop Client (The Creator)
* **Framework:** .NET MAUI (Multi-platform App UI)
* **Platform:** Windows Machine
* **Key Features:**
    * Creator Login.
    * Dashboard with dynamic playlist loading.
    * Content Upload (Music/Video) linked to API.

3. Mobile Client (The User)
* **Platform:** Android Native
* **Language:** Java
* **Networking:** Retrofit 2 + Gson
* **Key Features:**
    * RecyclerView for high-performance listing.
    * Deep navigation (Master-Detail flow).
    * Consumption of nested JSON data (Playlists containing Songs).

How to Run

Prerequisites
* .NET SDK 8.0
* Visual Studio Code (with C# Dev Kit)
* Android Studio (latest version)
* Java JDK 21

Step 1: Start the API
1.  Navigate to `PIMVIII/PimSystem/PimAPI`.
2.  Run the command:
    ```bash
    dotnet run
    ```
3.  Ensure the API is listening on **http://localhost:5207**.

Step 2: Run the Desktop App (Creator)
1.  Open `PIMVIII` in VS Code.
2.  Select `PimCreatorApp` as the startup project.
3.  Press **F5** to run on Windows Machine.
4.  **Login:** `admin@pim.com` (Create via Swagger if DB is empty).

Step 3: Run the Android App (User)
1.  Open `PIMVIII_Android_Studio` in Android Studio.
2.  Ensure your Emulator is running.
3.  Press **Run (Shift+F10)**.
4.  *Note:* The app connects to `10.0.2.2:5207` to access the local API.

Project Structure

```text
PJT_PIM_VIII
 ┣ PIMVIII (VS Code Workspace)
 ┃ ┣ PimSystem
 ┃ ┃ ┣ PimAPI (ASP.NET Core)
 ┃ ┃ ┗ PimCreatorApp (.NET MAUI)
 ┣ PIMVIII_Android_Studio (Android Project)
 ┃ ┗ app (Java Source Code)
