using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiNetCore6.Migrations
{
    public partial class updatebookingOnline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookingOnline_HotelId",
                table: "BookingOnline",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingOnline_Hotels_HotelId",
                table: "BookingOnline",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingOnline_Hotels_HotelId",
                table: "BookingOnline");

            migrationBuilder.DropIndex(
                name: "IX_BookingOnline_HotelId",
                table: "BookingOnline");
        }
    }
}
