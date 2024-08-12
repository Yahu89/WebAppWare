using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepAppWare.Database.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsFlows_WarehouseMovements_WarehouseMovementId",
                table: "ProductsFlows");

            migrationBuilder.DropTable(
                name: "WarehouseMovements");

            migrationBuilder.CreateTable(
                name: "WarehouseMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovementType = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseMovements", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsFlows_WarehouseMovements_WarehouseMovementId",
                table: "ProductsFlows",
                column: "WarehouseMovementId",
                principalTable: "WarehouseMovements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsFlows_WarehouseMovements_WarehouseMovementId",
                table: "ProductsFlows");

            migrationBuilder.DropTable(
                name: "WarehouseMovements");

            migrationBuilder.CreateTable(
                name: "WarehouseMovement",
                columns: table => new
                {
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovementType = table.Column<int>(type: "int", nullable: false),
                    TempId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.UniqueConstraint("AK_WarehouseMovement_TempId", x => x.TempId);
                    table.ForeignKey(
                        name: "FK_WarehouseMovement_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsFlows_WarehouseMovement_WarehouseMovementId",
                table: "ProductsFlows",
                column: "WarehouseMovementId",
                principalTable: "WarehouseMovement",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
