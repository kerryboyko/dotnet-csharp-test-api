# Tenant Complaints API

A simple REST API built with ASP.NET Core and Entity Framework Core for managing tenants and maintenance complaints.

## Getting Started

```bash
dotnet run
```
Then open http://localhost:5200/swagger

## Features

* Create and list tenants
* Create and list complaints
* Relational data: complaints include associated tenant information
* SQLite database with code-first migrations
* Interactive API documentation via Swagger

## Tech Stack

* ASP.NET Core Web API
* Entity Framework Core (SQLite)
* C#

## Example Endpoints

* `GET /tenants`
* `POST /tenants`
* `GET /complaints`
* `POST /complaints`

## Roadmap

* Add a React frontend for interacting with the API
* Implement server-side filtering for complaints (status, priority, tenant)
* Add authentication (e.g., OAuth)

## Notes

This project was built as part of learning the .NET ecosystem and demonstrates a full backend pipeline:
API → database → relational data → JSON responses.
