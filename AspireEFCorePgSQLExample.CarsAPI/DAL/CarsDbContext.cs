using AspireEFCorePgSQLExample.CarsAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace AspireEFCorePgSQLExample.CarsAPI.DAL;

public class CarsDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Maker> Makers { get; set; }

    public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("carsapi");

        Maker firstMaker = new(new Guid("faed6037-313d-439b-bd59-991827c3a6ed"), "Audi", "Germany");
        Maker secondMaker = new(new Guid("ccdfb6b5-2109-4b40-9c36-c0d2d0d3755d"), "BMW", "Germany");
        modelBuilder.Entity<Maker>(
            eb => 
            {
                eb.ToTable("makers");
                eb.HasKey(x => x.Guid)
                    .HasName("makers_pk");
                eb.HasAlternateKey(c => c.Name)
                    .HasName("makers_ak1_name");
                eb.Property(x => x.Guid)
                    .HasColumnName("maker_guid")
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                eb.Property(x => x.Name)
                    .HasColumnName("maker_name")
                    .IsRequired();
                eb.Property(x => x.Country)
                    .HasColumnName("maker_country")
                    .IsRequired();
                eb.Property(x => x.Update)
                    .HasColumnName("maker_update")
                    .ValueGeneratedOnAddOrUpdate();
                eb.Property(x => x.Insert)
                    .HasColumnName("maker_insert")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("NOW()");
                eb.HasData(firstMaker, secondMaker);
            });


        Car firstCar = new(new Guid("20028c57-8d63-47bf-89c9-197781983536"), "Audi R8", 2006, new Guid("faed6037-313d-439b-bd59-991827c3a6ed"));
        Car secondCar = new(new Guid("4a5d94a3-dba4-447c-bb9a-fea2077de64a"), "BMW E46 M3", 2001, new Guid("ccdfb6b5-2109-4b40-9c36-c0d2d0d3755d"));
        modelBuilder.Entity<Car>(
            eb =>
            {
                eb.ToTable("cars");
                eb.HasKey(x => x.Guid)
                    .HasName("cars_pk");
                eb.HasAlternateKey(c => new { c.MakerGuid, c.Name })
                    .HasName("cars_ak1_name_maker");
                eb.Property(x => x.Guid)
                    .HasColumnName("car_guid")
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                eb.Property(x => x.Name)
                    .HasColumnName("car_name")
                    .IsRequired()
                    .HasMaxLength(30);
                eb.Property(x => x.ReleaseYear)
                    .HasColumnName("car_releaseyear")
                    .IsRequired();
                eb.Property(x => x.MakerGuid)
                    .HasColumnName("car_makerguid")
                    .IsRequired();
                eb.Property(x => x.Update)
                    .HasColumnName("car_update")
                    .ValueGeneratedOnAddOrUpdate();
                eb.Property(x => x.Insert)
                    .HasColumnName("car_insert")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("NOW()");
                eb.HasOne(x => x.Maker)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.MakerGuid)
                    .HasConstraintName("cars_fk1_maker")
                    .OnDelete(DeleteBehavior.Cascade);
                eb.HasData(firstCar, secondCar);
            });
    }
}
