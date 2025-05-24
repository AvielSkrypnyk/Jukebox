# MPAJukebox 🎵

MPAJukebox is a web-based music jukebox application built with ASP.NET Core and Entity Framework Core. Users can browse songs by genre, create and manage playlists, and register/login to save their playlists.

## Features

- 🎶 Browse songs by genre
- 📋 Create, rename, and clear playlists
- 💾 Save playlists to your account (requires registration)
- 🔐 User registration and login
- 🕵️ View song details
- 🎨 Responsive UI with Bootstrap

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)

> **Note:**  
> This project is configured to use a MySQL database running locally with the following connection string (see [`appsettings.json`](appsettings.json)):
>
> ```
> "server=localhost;database=jukeboxdb;user=root;password=00000000;"
> ```
>
> If you are running this on another machine, you must update the connection string in [`appsettings.json`](appsettings.json) to match your MySQL server settings.

### Setup

1. **Clone the repository:**
    ```sh
    git clone https://github.com/AvielSkrypnyk/Jukebox.git
    cd Jukebox
    ```

2. **Configure the database:**

    Edit [`appsettings.json`](appsettings.json) and update the `DefaultConnection` string if needed.

3. **Apply database migrations:**
    ```sh
    dotnet ef database update
    ```

4. **Run the application:**
    ```sh
    dotnet run
    ```

5. **Open in your browser:**

    Navigate to [http://localhost:7269](http://localhost:7269) or the URL shown in the terminal.

## Project Structure

- Main entry: [`Program.cs`](Program.cs)
- Controllers: [`Controllers/`](Controllers/)
- Models: [`Models/`](Models/)
- Views: [`Views/`](Views/)
- Static assets: [`wwwroot/`](wwwroot/)

## License

This project is licensed under the MIT License. See [`LICENSE`](wwwroot/lib/bootstrap/LICENSE) and other library licenses in the `wwwroot/lib/` folders.

---

_Made by Aviel_