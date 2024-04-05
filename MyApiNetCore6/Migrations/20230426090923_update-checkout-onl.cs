using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiNetCore6.Migrations
{
    public partial class updatecheckoutonl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Onl_Amount",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Onl_BankCode",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Onl_OrderId",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Onl_OrderInfo",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Onl_PayDate",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Onl_SecureHash",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Onl_TransactionNo",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Onl_TransactionStatus",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Onl_Amount",
                table: "BookingOnline");

            migrationBuilder.DropColumn(
                name: "Onl_BankCode",
                table: "BookingOnline");

            migrationBuilder.DropColumn(
                name: "Onl_OrderId",
                table: "BookingOnline");

            migrationBuilder.DropColumn(
                name: "Onl_OrderInfo",
                table: "BookingOnline");

            migrationBuilder.DropColumn(
                name: "Onl_PayDate",
                table: "BookingOnline");

            migrationBuilder.DropColumn(
                name: "Onl_SecureHash",
                table: "BookingOnline");

            migrationBuilder.DropColumn(
                name: "Onl_TransactionNo",
                table: "BookingOnline");

            migrationBuilder.DropColumn(
                name: "Onl_TransactionStatus",
                table: "BookingOnline");
        }
    }
}
