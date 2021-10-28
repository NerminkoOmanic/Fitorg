# Introduction
Faculty project for course Software development I. This is just part of application with fully implemented administrator and partially trainers functionalities. Functionalities which should concern clients and part of trainers is not done because it was assignement for other student which did not finished it.
Fitorg is a web app which provides trainers help in organization of their clients, provide help in organization and assignment of trainings. Keeps history of training for better analysis

# Getting Started
Fitorg is .NET Core Web application developed using MVC pattern.  
Database is set up with EntityFramework.  
Authorization and authentification is added thorough Identity Server.  
2WayAuthentification is also provided.

# Build and Test

## Build locally :
1. Create database on your local SQL Server
2. Edit connection string so it connects to database which is on your local SQL Server
3. Migration of classes to database tables can be done using [EF Core](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs)
4. Build application and test it locally

## Test online
Application is available at link [Fitorg](https://fitorg.azurewebsites.net/)  
Online you can register and test functionalities which are available for trainers  
For info to log in as **administrator** contact me over some of provided adresses on my profile.  
**NOTE: CI/CD is fully done over azure devops, access to azure devops project can be provided also**
