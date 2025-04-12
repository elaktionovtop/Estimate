using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estimate.Migrations
{
    /// <inheritdoc />
    public partial class AllModels_Corr_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MeasureUnits_MeasureUnitId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderMaterials_Materials_MaterialId",
                table: "OrderMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderMaterials_Orders_OrderId",
                table: "OrderMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderWorks_Orders_OrderId",
                table: "OrderWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderWorks_Works_WorkId",
                table: "OrderWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_MeasureUnits_MeasureUnitId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_MeasureUnitId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Materials_MeasureUnitId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MeasureUnitId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderWorks");

            migrationBuilder.DropColumn(
                name: "MeasureUnitId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CompletionDate",
                table: "Orders",
                type: "date",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_UnitId",
                table: "Works",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UnitId",
                table: "Materials",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MeasureUnits_UnitId",
                table: "Materials",
                column: "UnitId",
                principalTable: "MeasureUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMaterials_Materials_MaterialId",
                table: "OrderMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMaterials_Orders_OrderId",
                table: "OrderMaterials",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWorks_Orders_OrderId",
                table: "OrderWorks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWorks_Works_WorkId",
                table: "OrderWorks",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_MeasureUnits_UnitId",
                table: "Works",
                column: "UnitId",
                principalTable: "MeasureUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MeasureUnits_UnitId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderMaterials_Materials_MaterialId",
                table: "OrderMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderMaterials_Orders_OrderId",
                table: "OrderMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderWorks_Orders_OrderId",
                table: "OrderWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderWorks_Works_WorkId",
                table: "OrderWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_MeasureUnits_UnitId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_UnitId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Materials_UnitId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "MeasureUnitId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderWorks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MeasureUnitId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Works_MeasureUnitId",
                table: "Works",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MeasureUnitId",
                table: "Materials",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MeasureUnits_MeasureUnitId",
                table: "Materials",
                column: "MeasureUnitId",
                principalTable: "MeasureUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMaterials_Materials_MaterialId",
                table: "OrderMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMaterials_Orders_OrderId",
                table: "OrderMaterials",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWorks_Orders_OrderId",
                table: "OrderWorks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWorks_Works_WorkId",
                table: "OrderWorks",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_MeasureUnits_MeasureUnitId",
                table: "Works",
                column: "MeasureUnitId",
                principalTable: "MeasureUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
