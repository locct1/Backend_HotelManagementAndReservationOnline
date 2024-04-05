using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiNetCore6.Migrations
{
    public partial class changroomreservationproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "RoomReservationProduct",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomReservationProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "RoomReservationProduct",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "RoomReservationProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "RoomReservationProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomRervationId",
                table: "RoomReservationProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RoomReservationProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservationProduct_ProductId",
                table: "RoomReservationProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservationProduct_RoomRervationId",
                table: "RoomReservationProduct",
                column: "RoomRervationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservationProduct_Product_ProductId",
                table: "RoomReservationProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservationProduct_RoomReservation_RoomRervationId",
                table: "RoomReservationProduct",
                column: "RoomRervationId",
                principalTable: "RoomReservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservationProduct_Product_ProductId",
                table: "RoomReservationProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservationProduct_RoomReservation_RoomRervationId",
                table: "RoomReservationProduct");

            migrationBuilder.DropIndex(
                name: "IX_RoomReservationProduct_ProductId",
                table: "RoomReservationProduct");

            migrationBuilder.DropIndex(
                name: "IX_RoomReservationProduct_RoomRervationId",
                table: "RoomReservationProduct");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "RoomReservationProduct");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RoomReservationProduct");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "RoomReservationProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "RoomReservationProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "RoomReservationProduct");

            migrationBuilder.DropColumn(
                name: "RoomRervationId",
                table: "RoomReservationProduct");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RoomReservationProduct");
        }
    }
}
