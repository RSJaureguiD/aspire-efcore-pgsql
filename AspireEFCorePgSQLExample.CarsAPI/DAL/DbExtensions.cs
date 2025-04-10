﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace AspireEFCorePgSQLExample.CarsAPI.DAL;

public static class DbExtensions
{
    public static async Task ConfigureDatabaseAsync(this WebApplication app)
    { 
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CarsDbContext>();

        await EnsureDatabaseAsync(dbContext);
        await RunMigrationsAsync(dbContext);
    }

    private static async Task EnsureDatabaseAsync(CarsDbContext dbContext)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync())
                await dbCreator.CreateAsync();
        });
    }

    private static async Task RunMigrationsAsync(CarsDbContext dbContext)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            await dbContext.Database.MigrateAsync();
            await transaction.CommitAsync();
        }
    }
}
