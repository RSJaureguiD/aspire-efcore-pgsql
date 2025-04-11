using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireEFCorePgSQLExample.CarsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE FUNCTION carsapi.cars_syncupdated() RETURNS trigger AS $$
            BEGIN
                NEW.car_update := NOW();
                RETURN NEW;
            END;
            $$ LANGUAGE plpgsql;

            CREATE TRIGGER
                cars_syncupdated_insert
            BEFORE INSERT ON
                carsapi.cars
            FOR EACH ROW EXECUTE PROCEDURE
                carsapi.cars_syncupdated();

            CREATE TRIGGER
                cars_syncupdated_update
            BEFORE UPDATE ON
                carsapi.cars
            FOR EACH ROW EXECUTE PROCEDURE
                carsapi.cars_syncupdated();

            CREATE FUNCTION carsapi.makers_syncupdated() RETURNS trigger AS $$
            BEGIN
                NEW.maker_update := NOW();
                RETURN NEW;
            END;
            $$ LANGUAGE plpgsql;

            CREATE TRIGGER
                makers_syncupdated_insert
            BEFORE INSERT ON
                carsapi.makers
            FOR EACH ROW EXECUTE PROCEDURE
                carsapi.makers_syncupdated();

            CREATE TRIGGER
                makers_syncupdated_update
            BEFORE UPDATE ON
                carsapi.makers
            FOR EACH ROW EXECUTE PROCEDURE
                carsapi.makers_syncupdated();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP TRIGGER IF EXISTS cars_syncupdated_insert ON carsapi.cars;
            DROP TRIGGER IF EXISTS cars_syncupdated_update ON carsapi.cars;
            DROP FUNCTION IF EXISTS carsapi.cars_syncupdated();
            DROP TRIGGER IF EXISTS makers_syncupdated_insert ON carsapi.makers;
            DROP TRIGGER IF EXISTS makers_syncupdated_update ON carsapi.makers;
            DROP FUNCTION IF EXISTS makers_syncupdated();"
            );
        }
    }
}
