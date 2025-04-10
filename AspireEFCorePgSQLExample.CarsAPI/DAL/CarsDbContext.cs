using Microsoft.EntityFrameworkCore;

namespace AspireEFCorePgSQLExample.CarsAPI.DAL;

public class CarsDbContext : DbContext
{
    public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
    {
        
    }
}
