# .NET Aspire + EntityFramework Core + PostgreSQL Example
This is just a personal exercise to learn how to use EntityFramework Core with PostgreSQL in a .NET Aspire project. But it can be used as a reference for anyone who wants to do the same.

It sets up a simple api for cars and manufacturers with a one to many relationship. The database is created using code first migrations, and it uses Scalar to present the endpoints and their documentation.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started)

## Windows Instructions

1. Have Docker installed and running.
2. Clone the repository.

```bash
git clone https://github.com/RSJaureguiD/aspire-efcore-pgsql.git
```

3. cd into the AppHost project directory.

```bash
cd .\aspire-efcore-pgsql\AspireEFCorePgSQLExample.AppHost
```

4. Setup the password as a user secret

```bash
dotnet user-secrets set Parameters:pgsql-password your_password_here
```

5. Build the project

```bash
dotnet build
```

6. Run the project

```bash
dotnet run
```

The output will provide you with the URL to access the Aspire dashboard, where you can access the API itself and a pgAdmin instance to manage the database.

## API Endpoints

The API exposes the following endpoints:

- Scalar docs
    - `GET /scalar/v1`: Get the API documentation with Scalar

- Cars
    - `GET /api/cars`: Get all cars
    - `GET /api/cars/{id}`: Get a car by id
    - `POST /api/cars`: Create a new car
    - `PUT /api/cars/{id}`: Update a car by id
    - `DELETE /api/cars/{id}`: Delete a car by id

- Manufacturer
    - `GET /api/maker`: Get all manufacturers
    - `GET /api/maker/{id}`: Get a manufacturer by id
    - `POST /api/maker`: Create a new manufacturer
    - `PUT /api/maker/{id}`: Update a manufacturer by id
    - `DELETE /api/maker/{id}`: Delete a manufacturer by id

