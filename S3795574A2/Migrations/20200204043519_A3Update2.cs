using Microsoft.EntityFrameworkCore.Migrations;

namespace S3795574A2.Migrations
{
    public partial class A3Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Logins");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Logins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
