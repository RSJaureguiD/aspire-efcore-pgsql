using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

using OpenTelemetry.Trace;

using AspireEFCorePgSQLExample.CarsAPI.DAL;
using AspireEFCorePgSQLExample.CarsAPI.DAL.Models;

namespace AspireEFCorePgSQLExample.MigrationService;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CarsDbContext>();

            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(CarsDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(CarsDbContext dbContext, CancellationToken cancellationToken)
    {
        Maker firstMaker = new(Guid.NewGuid(), "Audi", "Germany");
        Maker secondMaker = new(Guid.NewGuid(), "BMW", "Germany");

        Car firstCar = new(Guid.NewGuid(), "Audi R8", 2006, firstMaker.Guid);
        Car secondCar = new(Guid.NewGuid(), "BMW E46 M3", 2001, secondMaker.Guid);

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Makers.AddAsync(firstMaker, cancellationToken);
            await dbContext.Makers.AddAsync(secondMaker, cancellationToken);
            await dbContext.Cars.AddAsync(firstCar, cancellationToken);
            await dbContext.Cars.AddAsync(secondCar, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}