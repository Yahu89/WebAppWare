using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepAppWare.Database.Migrations
{
    /// <inheritdoc />
    public partial class StructureChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "WarehouseMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_WarehouseId",
                table: "WarehouseMovements",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseMovements_Warehouses_WarehouseId",
                table: "WarehouseMovements",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

			migrationBuilder.DropColumn(
				name: "WarehouseId",
				table: "ProductsFlows");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseMovements_Warehouses_WarehouseId",
                table: "WarehouseMovements");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseMovements_WarehouseId",
                table: "WarehouseMovements");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "WarehouseMovements");
        }
    }
}
