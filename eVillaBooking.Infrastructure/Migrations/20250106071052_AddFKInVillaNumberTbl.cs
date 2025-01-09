using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVillaBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFKInVillaNumberTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Villa_Id",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_Villa_Id",
                table: "VillaNumbers",
                column: "Villa_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_Villa_Id",
                table: "VillaNumbers",
                column: "Villa_Id",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_Villa_Id",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_Villa_Id",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "Villa_Id",
                table: "VillaNumbers");
        }
    }
}
