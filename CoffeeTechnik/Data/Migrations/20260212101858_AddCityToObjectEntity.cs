using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeTechnik.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCityToObjectEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Objects");
        }


    }
}
