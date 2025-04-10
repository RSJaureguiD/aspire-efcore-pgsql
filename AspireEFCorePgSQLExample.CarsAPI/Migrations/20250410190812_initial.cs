using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireEFCorePgSQLExample.CarsAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "carsapi");

            migrationBuilder.CreateTable(
                name: "makers",
                schema: "carsapi",
                columns: table => new
                {
                    maker_guid = table.Column<Guid>(type: "uuid", nullable: false),
                    maker_name = table.Column<string>(type: "text", nullable: false),
                    maker_country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("makers_pk", x => x.maker_guid);
                    table.UniqueConstraint("makers_ak1_name", x => x.maker_name);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                schema: "carsapi",
                columns: table => new
                {
                    car_guid = table.Column<Guid>(type: "uuid", nullable: false),
                    car_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    car_releaseyear = table.Column<int>(type: "integer", nullable: false),
                    car_makerguid = table.Column<Guid>(type: "uuid", nullable: false),
                    car_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    car_insert = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cars_pk", x => x.car_guid);
                    table.UniqueConstraint("cars_ak1_name_maker", x => new { x.car_makerguid, x.car_name });
                    table.ForeignKey(
                        name: "cars_fk1_maker",
                        column: x => x.car_makerguid,
                        principalSchema: "carsapi",
                        principalTable: "makers",
                        principalColumn: "maker_guid",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cars",
                schema: "carsapi");

            migrationBuilder.DropTable(
                name: "makers",
                schema: "carsapi");
        }
    }
}
