# Introduction
Faculty project for course Software development I. This is just part of application with fully implemented administrator and partially trainers functionalities. Functionalities which should concern clients and part of trainers is not done because it was assignement for other student which did not finish it.
Fitorg is a web app which provides trainers help in organization of their clients, provide help in organization and assignment of trainings. Keeps history of training for better analysis

# Getting Started
Fitorg is .NET Core Web application developed using MVC pattern.  
Database is set up with EntityFramework.  
Authorization and authentification is added thorough Identity Server.  
2WayAuthentification is also provided.

# Build and Test

## Build locally :
1. Create database on your local SQL Server
2. Edit [connection string](/FITorg.Web/appsettings.json) so it connects to database which is on your local SQL Server
3. Migration of classes to database tables can be done using [EF Core](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs)
4. Build application and test it locally

## Test online
Application is available at link [Fitorg](https://fitorg.azurewebsites.net/)  
Online you can register and test functionalities which are available for trainers  
For info to log in as **administrator** contact me over some of provided adresses on my profile.  
**NOTE: CI/CD is fully done over azure devops, access to azure devops project can be provided also**


# ScreenShots

## Administrator

Administrator's start page after log in  
![AdminStart](/fitorgss/adminSettings.png)

List of all administrators with options to edit(uredi) or delete(obriši) specific admin, also to add(dodaj admina) a new one 
![AdminListAdmins](/fitorgss/adminListAdmins.png)

List of all cities which are available to chose during registration. With options to edit(uredi), delete(obriši), add(dodaj grad)
Administrator has also available lists of countries and genders with same controls on them like for cities
![AdminListCities](/fitorgss/adminListCities.png)

## Coach

Coach's start page after log in with personal details and option to edit(uredi) them  
![CoachStart](/fitorgss/TrenerStart.png)

Coach's list of excercises, with options to edit(uredi), delete(obrisi) and add(dodaj vjezbu)   
Coach can add video of excercise from youtube, and by clicking on link inside table it will open video under for watching  
![Coachexcercises](/fitorgss/TrenerVjezbe.png)

Coach's list of clients, add button is not implemented cause it was assignemenet of other student  
![Coachexcercises](/fitorgss/TrenerListClients.png)

## Change password

Both administrator and coach can change their passwords  
![ChangePassword](/fitorgss/adminChangePassword.png)

## 2 factor authentification

2FA is available for all users  
![2FA](/fitorgss/admin2FA.png)
![2FA](/fitorgss/admin2FAConfigure.png)
