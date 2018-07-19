# Shelf Share

Shelf Share is am ASP.NET Core web app for users to share the books they own with other registered users. The application stores book information in a database with information pulled from the Goodreads API. Registered users are able to look up books in the Shelf Share database, add new books to the database via information from the Goodreads API, add and delete books from their own shelf, and request to follow other users.

**[LIVE DEMO](https://shelfshareapp.azurewebsites.net)**
### Built with
- ASP.NET Core 2.1
- Microsoft Sql Server
- Bootstrap
- jQuery
- Goodreads API

### Getting started

##### Prerequisites to run locally:

- .NET Core SDK 2.1 or higher: https://www.microsoft.com/net/download
- A version of Visual Studio or Visual Studio Code
- Goodreads API Key https://www.goodreads.com/api
- Microsoft SQL Server/SSMS

##### To run the application:

- Run CreateTables.sql and StoredProcedures.txt in SSMS
- Configure connection string and Goodreads API key in user secrets
 ```
 dotnet restore
 dotnet build
 dotnet run
```
