using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eVillaBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaNumberInTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VillaNumbers",
                columns: new[] { "Villa_Number", "SpecialDetails", "Villa_Id" },
                values: new object[,]
                {
                    { 101, null, 1 },
                    { 201, null, 2 },
                    { 301, null, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 301);
        }
    }
}
