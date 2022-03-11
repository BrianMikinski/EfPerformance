# EfPerformance
Entity Framework Performance Testing



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

