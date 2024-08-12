using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepAppWare.Database.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseToIdcolumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseToId",
                table: "WarehouseMovements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_WarehouseToId",
                table: "WarehouseMovements",
                column: "WarehouseToId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseMovements_Warehouses_WarehouseToId",
                table: "WarehouseMovements",
                column: "WarehouseToId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseMovements_Warehouses_WarehouseToId",
                table: "WarehouseMovements");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseMovements_WarehouseToId",
                table: "WarehouseMovements");

            migrationBuilder.DropColumn(
                name: "WarehouseToId",
                table: "WarehouseMovements");
        }
    }
}
