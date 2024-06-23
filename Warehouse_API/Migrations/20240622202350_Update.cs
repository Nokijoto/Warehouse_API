using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse_API.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RFIDTag_Products_ProductId",
                schema: "Warehouse",
                table: "RFIDTag");

            migrationBuilder.DropIndex(
                name: "IX_RFIDTag_ProductId",
                schema: "Warehouse",
                table: "RFIDTag");

            migrationBuilder.DropIndex(
                name: "IX_Products_RFIDTagId",
                schema: "Warehouse",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                schema: "Warehouse",
                table: "RFIDTag",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RFIDTagId",
                schema: "Warehouse",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDTag_ProductId",
                schema: "Warehouse",
                table: "RFIDTag",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_RFIDTagId",
                schema: "Warehouse",
                table: "Products",
                column: "RFIDTagId",
                unique: true,
                filter: "[RFIDTagId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RFIDTag_Products_ProductId",
                schema: "Warehouse",
                table: "RFIDTag",
                column: "ProductId",
                principalSchema: "Warehouse",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RFIDTag_Products_ProductId",
                schema: "Warehouse",
                table: "RFIDTag");

            migrationBuilder.DropIndex(
                name: "IX_RFIDTag_ProductId",
                schema: "Warehouse",
                table: "RFIDTag");

            migrationBuilder.DropIndex(
                name: "IX_Products_RFIDTagId",
                schema: "Warehouse",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                schema: "Warehouse",
                table: "RFIDTag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RFIDTagId",
                schema: "Warehouse",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RFIDTag_ProductId",
                schema: "Warehouse",
                table: "RFIDTag",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_RFIDTagId",
                schema: "Warehouse",
                table: "Products",
                column: "RFIDTagId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RFIDTag_Products_ProductId",
                schema: "Warehouse",
                table: "RFIDTag",
                column: "ProductId",
                principalSchema: "Warehouse",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
