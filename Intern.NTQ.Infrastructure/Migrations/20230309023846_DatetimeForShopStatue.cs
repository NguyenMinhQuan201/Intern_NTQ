﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intern.NTQ.Infrastructure.Migrations
{
    public partial class DatetimeForShopStatue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Shops");
        }
    }
}
