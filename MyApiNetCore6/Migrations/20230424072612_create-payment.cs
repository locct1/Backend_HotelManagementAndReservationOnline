using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiNetCore6.Migrations
{
    public partial class createpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MethodPaymentId",
                table: "BookingOnline",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MethodPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodPayment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingOnline_MethodPaymentId",
                table: "BookingOnline",
                column: "MethodPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingOnline_MethodPayment_MethodPaymentId",
                table: "BookingOnline",
                column: "MethodPaymentId",
                principalTable: "MethodPayment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingOnline_MethodPayment_MethodPaymentId",
                table: "BookingOnline");

            migrationBuilder.DropTable(
                name: "MethodPayment");

            migrationBuilder.DropIndex(
                name: "IX_BookingOnline_MethodPaymentId",
                table: "BookingOnline");

            migrationBuilder.DropColumn(
                name: "MethodPaymentId",
                table: "BookingOnline");
        }
    }
}
