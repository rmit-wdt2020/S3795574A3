using Microsoft.EntityFrameworkCore.Migrations;

namespace S3795574A2.Migrations
{
    public partial class A3Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "BillPays",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Accounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "BillPays");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Accounts");
        }
    }
}
