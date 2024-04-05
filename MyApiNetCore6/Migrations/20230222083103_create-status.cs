using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiNetCore6.Migrations
{
    public partial class createstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "BookingOnline",
                newName: "StatusId");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingOnline_StatusId",
                table: "BookingOnline",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingOnline_Status_StatusId",
                table: "BookingOnline",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingOnline_Status_StatusId",
                table: "BookingOnline");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_BookingOnline_StatusId",
                table: "BookingOnline");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "BookingOnline",
                newName: "Status");
        }
    }
}
