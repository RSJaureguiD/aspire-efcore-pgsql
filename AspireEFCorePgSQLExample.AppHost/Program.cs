var builder = DistributedApplication.CreateBuilder(args);

var pgsqlPassword = builder.AddParameter("pgsql-password", secret: true);

var pgsql = builder.AddPostgres("pgsql", password: pgsqlPassword)
    .WithDataVolume();

var pgsqldb = pgsql.AddDatabase("pgsqldb");

builder.AddProject<Projects.AspireEFCorePgSQLExample_CarsAPI>("carsapi")
    .WithReference(pgsqldb)
    .WaitFor(pgsqldb);

builder.AddProject<Projects.AspireEFCorePgSQLExample_MigrationService>("migrations")
    .WithReference(pgsqldb)
    .WaitFor(pgsqldb);

builder.Build().Run();
