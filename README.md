# EfPerformance
Entity Framework Performance Testing

## NOTES ON RUNNING PERFORMANCE QUERIES
- Close out of all other sql connection related apps. I have seen multiple connections affect db performance for a single server
- Make sure your DB's are both correct for EF6 and EF Core

## create new angular app
dotnet new angular --name DotNet6AngularTemplate

## create a new solution file
dotnet new sln 

## add project to new solution file
dotnet sln add [path to project]

## Create new library
dotnet new classlib --name NameOfLibrary

## scaffold a local db context
dotnet ef dbcontext scaffold "Server=(localdb)\mssqllocaldb;Database=CoreBlogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models

## add a reference to another dotnet project
 dotnet add .\EfPerformance.csproj reference  ..\CoreBlog\CoreBlog.csproj
