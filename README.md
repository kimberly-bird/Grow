# Welcome to Grow!

## Overview

Grow is my attempt to build an app that will help me track my houseplants. My husband got me a houseplant monthly subscription and it's been challenging to keep the plants alive and thriving! The goal with this app is to enter each of your plants, along with periodic updates to optimize the watering, lighting, and other needs of each plant.

Grow implements the Identity framework, and extends the base User object with the `ApplicationUser` model.
It shows how to remove a model's property from the automatic model binding in a controller method by using `ModelState.Remove()`.
Make sure you look in the `DbInitializer` class to see the product types that are seeded for you.

This app uses CloudinaryDotNet to upload images and stores the URL in the Grow Database. Visit their repo here to learn more about Cloudinary: https://github.com/cloudinary/CloudinaryDotNet

## Setup

### Git and SQL Server Configuration

1. Clone this repository to your machine.
1. Create a new repository.
1. Copy the connection string for your repo.
1. From your project directory, execute the following commands
    ```sh
    git remote remove origin
    git remote add origin <paste Github URL here>
    ```
1. Push up the master branch to your new remote origin
1. Create a branch named `initial-setup`.
1. Go into the project directory and set up your appsettings
    ```sh
    cd Bangazon
    dotnet restore
    cp appsettings.json.template appsettings.json
    ```
1. Open Visual Studio and has loaded the solution file
1. Once your IDE is running, you'll have to update your new `appsettings.json` file with the following content. Update to your SQL Server name.
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=YourServerHere\\SQLEXPRESS;Database=BangazonSite;Trusted_Connection=True;"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Warning"
        }
      },
      "AllowedHosts": "*"
    }
    ```

### Generating the Database

Once your appsettings are updated and you've entered in some seed data, you should generate your database.

1. Go to the Package Manager Console in Visual Studio.
1. Use the `Add-Migration GrowTables` command.
1. Once Visual Studio shows you the migration file, execute `Update-Database` to generate your tables.
1. Use the SQL Server Object Explorer to verify that everything worked as expected.

### Grow ERD
![ERD](GrowERD.png)
