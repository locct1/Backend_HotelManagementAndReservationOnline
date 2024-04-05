﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiNetCore6.Migrations
{
    public partial class changebookingOnline2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HotelAddress",
                table: "BookingOnline",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelAddress",
                table: "BookingOnline");
        }
    }
}