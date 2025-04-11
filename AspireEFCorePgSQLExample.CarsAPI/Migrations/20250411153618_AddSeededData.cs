using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspireEFCorePgSQLExample.CarsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "carsapi",
                table: "makers",
                columns: new[] { "maker_guid", "maker_country", "maker_name" },
                values: new object[,]
                {
                    { new Guid("ccdfb6b5-2109-4b40-9c36-c0d2d0d3755d"), "Germany", "BMW" },
                    { new Guid("faed6037-313d-439b-bd59-991827c3a6ed"), "Germany", "Audi" }
                });

            migrationBuilder.InsertData(
                schema: "carsapi",
                table: "cars",
                columns: new[] { "car_guid", "car_makerguid", "car_name", "car_releaseyear" },
                values: new object[,]
                {
                    { new Guid("20028c57-8d63-47bf-89c9-197781983536"), new Guid("faed6037-313d-439b-bd59-991827c3a6ed"), "Audi R8", 2006 },
                    { new Guid("4a5d94a3-dba4-447c-bb9a-fea2077de64a"), new Guid("ccdfb6b5-2109-4b40-9c36-c0d2d0d3755d"), "BMW E46 M3", 2001 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "carsapi",
                table: "cars",
                keyColumn: "car_guid",
                keyValue: new Guid("20028c57-8d63-47bf-89c9-197781983536"));

            migrationBuilder.DeleteData(
                schema: "carsapi",
                table: "cars",
                keyColumn: "car_guid",
                keyValue: new Guid("4a5d94a3-dba4-447c-bb9a-fea2077de64a"));

            migrationBuilder.DeleteData(
                schema: "carsapi",
                table: "makers",
                keyColumn: "maker_guid",
                keyValue: new Guid("ccdfb6b5-2109-4b40-9c36-c0d2d0d3755d"));

            migrationBuilder.DeleteData(
                schema: "carsapi",
                table: "makers",
                keyColumn: "maker_guid",
                keyValue: new Guid("faed6037-313d-439b-bd59-991827c3a6ed"));
        }
    }
}
