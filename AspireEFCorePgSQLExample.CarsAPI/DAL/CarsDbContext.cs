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
            });

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
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
    }
}
