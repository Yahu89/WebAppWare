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
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductsFlows_Warehouses_WarehouseId",
            //    table: "ProductsFlows");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WarehouseMovements_Warehouses_WarehouseId",
            //    table: "WarehouseMovements");

            //migrationBuilder.DropIndex(
            //    name: "IX_ProductsFlows_WarehouseId",
            //    table: "ProductsFlows");

            //migrationBuilder.DropColumn(
            //    name: "WarehouseId",
            //    table: "ProductsFlows");

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

            //migrationBuilder.AddColumn<int>(
            //    name: "WarehouseId",
            //    table: "ProductsFlows",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductsFlows_WarehouseId",
            //    table: "ProductsFlows",
            //    column: "WarehouseId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductsFlows_Warehouses_WarehouseId",
            //    table: "ProductsFlows",
            //    column: "WarehouseId",
            //    principalTable: "Warehouses",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WarehouseMovements_Warehouses_WarehouseId",
            //    table: "WarehouseMovements",
            //    column: "WarehouseId",
            //    principalTable: "Warehouses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
