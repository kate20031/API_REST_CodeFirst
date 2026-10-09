# Movies and Series API

This is my ASP.NET Core project for the REST API module at IUT Annecy.

The project has two APIs:
- **Movies API** uses the `cinema` PostgreSQL database.
- **Series API** uses the `SeriesDB` PostgreSQL database.

I kept both APIs in the same project because they use different databases and tables.

## Series API

The Series API was developed for Session 5. It uses DTOs and AutoMapper to return data from the database.

Main endpoints:
- `GET /api/Series` — get a list of series.
- `GET /api/Series/{id}` — get details about one series.
- `GET /api/Series/Networks` — get statistics grouped by network.

## Technologies

- C#
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- AutoMapper
- xUnit and Moq

## Run the project

1. Set the database connection strings in `appsettings.json`.
2. Make sure both databases are running and contain the required tables.
3. Run the project and open Swagger to try the endpoints.

## Run tests

From the solution folder, run:

```bash
dotnet test
```
