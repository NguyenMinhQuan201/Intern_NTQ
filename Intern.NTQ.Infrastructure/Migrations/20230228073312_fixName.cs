using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intern.NTQ.Infrastructure.Migrations
{
    public partial class fixName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Users",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Reviews",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Reviews",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Reviews",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Products",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Products",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Products",
                newName: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Users",
                newName: "DeleteAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Reviews",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Reviews",
                newName: "DeleteAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Reviews",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Products",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Products",
                newName: "DeleteAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Products",
                newName: "CreateAt");
        }
    }
}
