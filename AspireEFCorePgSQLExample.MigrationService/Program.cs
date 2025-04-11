using AspireEFCorePgSQLExample.CarsAPI.DAL;
using AspireEFCorePgSQLExample.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));
builder.AddNpgsqlDbContext<CarsDbContext>("pgsqldb");

var host = builder.Build();
host.Run();
