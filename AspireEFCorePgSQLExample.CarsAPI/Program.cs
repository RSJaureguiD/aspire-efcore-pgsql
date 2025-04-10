using AspireEFCorePgSQLExample.CarsAPI.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<CarsDbContext>("pgsqldb");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.ConfigureDatabaseAsync();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
