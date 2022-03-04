using Microsoft.EntityFrameworkCore.Migrations;

namespace GraphQLApp.Migrations
{
    public partial class addplatformenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeEnum",
                table: "Platforms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeEnum",
                table: "Platforms");
        }
    }
}
