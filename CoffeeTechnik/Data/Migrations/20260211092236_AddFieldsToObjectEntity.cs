using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeTechnik.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToObjectEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bulstat",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bulstat",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Objects");
        }
    }
}
